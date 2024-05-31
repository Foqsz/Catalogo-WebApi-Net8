using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCatalogo.Catalogo.Application.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //executa antes do action
            _logger.LogInformation("##### Executando -> OnActionExecuting");
            _logger.LogInformation("###########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //executa apos o action
            _logger.LogInformation("##### Executando -> OnActionExecuted");
            _logger.LogInformation("###########################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"Status Code : {context.HttpContext.Response.StatusCode}");
        }
    }
}
