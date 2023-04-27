using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace tbcpay.services.Middlewares
{
    public class ActionRedirectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ActionRedirectionMiddleware> _logger;


        public ActionRedirectionMiddleware(RequestDelegate next, ILogger<ActionRedirectionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
{
    try
    {
        var action = context.Request.Query;
        var operation = action.First(x => x.Key == "command").Value;
        if (!string.IsNullOrEmpty(operation))
            context.Request.Path = $"/api/integration/{operation.ToLower()}";
    }

    catch (Exception ex)
    {
        _logger.LogInformation("{Message}", ex.Message);
    }

    await _next.Invoke(context);
}
    }
}
