using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApplicationWebUI.Filters
{
    public class ActionLoggingFilter : IAsyncActionFilter
    {
        private readonly ILogger<ActionLoggingFilter> _logger;

        public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
        {
            _logger = logger;
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

            if (executed.Exception == null || executed.ExceptionHandled)
            {
                _logger.LogInformation("Action end {Action} status={StatusCode} elapsed={Elapsed}ms trace={TraceId}",
                    displayName,
                    executed.HttpContext.Response.StatusCode,
                    sw.ElapsedMilliseconds,
                    executed.HttpContext.TraceIdentifier);
            }
            else
            {
                _logger.LogError(executed.Exception, "Action failed {Action} elapsed={Elapsed}ms trace={TraceId}",
                    displayName,
                    sw.ElapsedMilliseconds,
                    executed.HttpContext.TraceIdentifier);
            }
        }
    }
}
