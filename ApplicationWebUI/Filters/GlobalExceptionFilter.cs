using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ApplicationWebUI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, ITempDataDictionaryFactory tempDataFactory)
        {
            _logger = logger;
            _tempDataFactory = tempDataFactory;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var tempData = _tempDataFactory.GetTempData(context.HttpContext);

            if (exception is InvalidOperationException || exception is KeyNotFoundException)
            {
                tempData["ErrorMessage"] = exception.Message;
                var controller = context.RouteData.Values["controller"]?.ToString();
                if (string.IsNullOrWhiteSpace(controller))
                {
                    controller = "Home";
                }

                context.Result = new RedirectToActionResult("Index", controller, null);
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception.");
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }

            context.ExceptionHandled = true;
        }
    }
}


