using System.Diagnostics;
using BusinessLayer.Services.Abstractions;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace ApplicationWebUI.Filters
{
    public class ActionLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<ActionLoggingFilter> _logger;
        private readonly ILogService _logService;

        public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger, ILogService logService)
        {
            _logger = logger;
            _logService = logService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            var displayName = context.ActionDescriptor.DisplayName ?? "UnknownAction";
            var correlationId = context.HttpContext.TraceIdentifier;

            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Action start {Action} {Method} {Path} trace={TraceId}",
                displayName,
                request.Method,
                request.Path,
                context.HttpContext.TraceIdentifier);

            var executed = await next();

            sw.Stop();
            var log = BuildRequestLog(context, executed, sw.ElapsedMilliseconds, correlationId);

            if (executed.Exception == null || executed.ExceptionHandled)
            {
                _logger.LogInformation("Action end {Action} status={StatusCode} elapsed={Elapsed}ms trace={TraceId}",
                    displayName,
                    executed.HttpContext.Response.StatusCode,
                    sw.ElapsedMilliseconds,
                    executed.HttpContext.TraceIdentifier);
                await PersistAsync(log);
            }
            else
            {
                _logger.LogError(executed.Exception, "Action failed {Action} elapsed={Elapsed}ms trace={TraceId}",
                    displayName,
                    sw.ElapsedMilliseconds,
                    executed.HttpContext.TraceIdentifier);
                await PersistAsync(log);
            }
        }

        private static RequestLog BuildRequestLog(ActionExecutingContext ctx, ActionExecutedContext executed, long durationMs, string correlationId)
        {
            var request = ctx.HttpContext.Request;
            return new RequestLog
            {
                CreatedDate = DateTime.UtcNow,
                RequestPath = request.Path.HasValue ? request.Path.Value : string.Empty,
                HttpMethod = request.Method,
                StatusCode = executed.HttpContext.Response.StatusCode,
                LogLevel = executed.Exception == null || executed.ExceptionHandled ? "Information" : "Error",
                UserName = null,
                ClientIp = GetClientIp(ctx.HttpContext),
                UserAgent = GetUserAgent(ctx.HttpContext),
                CorrelationId = correlationId,
                DurationMs = durationMs
            };
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

        private async Task PersistAsync(RequestLog log)
        {
            try
            {
                await _logService.LogAsync(log);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to persist action log for {TraceId}", log.CorrelationId);
            }
        }
    }
}
