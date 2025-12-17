using System.Diagnostics;
using System.Text;
using System.Linq;
using BusinessLayer.Services.Abstractions;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace ApplicationWebUI.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private const int MaxBodyLength = 4096;
        private const string CorrelationIdHeader = "X-Correlation-ID";

        private static readonly PathString[] SkippedPaths = new[]
        {
            new PathString("/favicon.ico"),
            new PathString("/robots.txt"),
            new PathString("/.well-known"),
            new PathString("/health"),
            new PathString("/healthz")
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (ShouldSkipLogging(context.Request.Path))
            {
                await _next(context);
                return;
            }

            var sw = Stopwatch.StartNew();

            var correlationId = ResolveCorrelationId(context);

            var log = new RequestLog
            {
                CreatedDate = DateTime.UtcNow,
                RequestPath = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty,
                HttpMethod = context.Request.Method,
                QueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
                CorrelationId = correlationId,
                UserName = GetUserName(context),
                ClientIp = GetClientIp(context),
                UserAgent = GetUserAgent(context)
            };

            log.RequestBody = await ReadRequestBodyAsync(context.Request);

            var logged = false;

            try
            {
                await _next(context);

                log.StatusCode = context.Response.StatusCode;
                log.LogLevel = GetLevel(log.StatusCode);
            }
            catch (Exception ex)
            {
                log.StatusCode = StatusCodes.Status500InternalServerError;
                log.LogLevel = "Error";
                log.ErrorMessage = ex.Message;
                log.StackTrace = ex.ToString();
                log.DurationMs = sw.ElapsedMilliseconds;

                _logger.LogError(ex, "Unhandled exception for {Method} {Path} trace={TraceId}", log.HttpMethod, log.RequestPath, log.CorrelationId);

                await SafeLogAsync(context, log);
                logged = true;

                throw;
            }
            finally
            {
                if (!logged)
                {
                    log.DurationMs = sw.ElapsedMilliseconds;
                    await SafeLogAsync(context, log);
                }
            }
        }

        private static bool ShouldSkipLogging(PathString path)
        {
            foreach (var skipped in SkippedPaths)
            {
                if (path.StartsWithSegments(skipped, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ResolveCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId) &&
                !StringValues.IsNullOrEmpty(correlationId))
            {
                context.TraceIdentifier = correlationId!;
                return correlationId!;
            }

            return context.TraceIdentifier;
        }

        private static string? GetUserName(HttpContext context)
        {
            if (context.User?.Identity?.IsAuthenticated == true)
                return context.User.Identity?.Name;

            return null;
        }

        private static string? GetClientIp(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwarded) &&
                !StringValues.IsNullOrEmpty(forwarded))
            {
                var first = forwarded.ToString()
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(first))
                    return first;
            }

            return context.Connection.RemoteIpAddress?.ToString();
        }

        private static string? GetUserAgent(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("User-Agent", out var userAgent) &&
                !StringValues.IsNullOrEmpty(userAgent))
            {
                return userAgent.ToString();
            }

            return null;
        }

        private static string GetLevel(int statusCode)
        {
            if (statusCode >= 500) return "Error";
            if (statusCode >= 400) return "Warning";
            return "Information";
        }

        private async Task<string?> ReadRequestBodyAsync(HttpRequest request)
        {
            if (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method))
                return null;

            try
            {
                request.EnableBuffering();

                using var reader = new StreamReader(
                    request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: MaxBodyLength,
                    leaveOpen: true);

                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;

                var masked = MaskSensitive(body);

                if (masked.Length > MaxBodyLength)
                    return masked[..MaxBodyLength] + "...";

                return masked;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return "[Body not be read]";
            }
        }

        private static string MaskSensitive(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return body;

            var lowered = body.ToLowerInvariant();
            if (lowered.Contains("password") || lowered.Contains("sifre"))
                return "[MASKED]";

            return body;
        }

        private async Task SafeLogAsync(HttpContext context, RequestLog log)
        {
            var bodyState = string.IsNullOrEmpty(log.RequestBody) ? "none" : "present";

            _logger.Log(
                ToLogLevel(log.LogLevel),
                "HTTP {Method} {Path} => {StatusCode} in {Duration} ms | trace={TraceId} | user={User} | ip={ClientIp} | agent={UserAgent} | qs={QueryString} | body={BodyState}",
                log.HttpMethod,
                log.RequestPath,
                log.StatusCode,
                log.DurationMs,
                log.CorrelationId,
                log.UserName ?? "anonymous",
                log.ClientIp ?? "unknown",
                Truncate(log.UserAgent, 128),
                log.QueryString ?? string.Empty,
                bodyState);

            try
            {
                var logService = context.RequestServices.GetRequiredService<ILogService>();
                await logService.LogAsync(log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to persist request log for {TraceId}", log.CorrelationId);
            }
        }

        private static LogLevel ToLogLevel(string? level)
        {
            return level switch
            {
                "Error" => LogLevel.Error,
                "Warning" => LogLevel.Warning,
                _ => LogLevel.Information
            };
        }

        private static string? Truncate(string? value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            return value.Length <= maxLength ? value : value[..maxLength];
        }
    }
}
