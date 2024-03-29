namespace tbcpay.services.Middlewares;

public class ActionRedirectionMiddleware
{
    private readonly ILogger<ActionRedirectionMiddleware> _logger;
    private readonly RequestDelegate _next;


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
                context.Request.Path = context.Request.Path + '/' + operation;
        }

        catch (Exception ex)
        {
            _logger.LogInformation("{Message}", ex.Message);
        }

        await _next.Invoke(context);
    }
}