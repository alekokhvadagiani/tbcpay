namespace tbcpay.services.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    private readonly XmlWriterSettings _xmlSettings = new XmlWriterSettings { Async = true };

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogInformation(e, "An error occurred while processing the request.");

            var response = new BaseResponse
            {
                Comment = _env.IsDevelopment() ? e.Message : "Internal Server Error",
                Result = ProviderStatusCodes.GenericError
            };

            context.Response.ContentType = "application/xml";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await using var stringWriter = new StringWriter();
            using var writer = XmlWriter.Create(stringWriter, _xmlSettings);
            var xsSubmit = new XmlSerializer(typeof(BaseResponse));
            xsSubmit.Serialize(writer, response);

            await context.Response.WriteAsync(stringWriter.ToString());
        }
    }
}
