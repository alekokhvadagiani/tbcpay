using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tbcpay.services.Helpers;
using tbcpay.services.Dto.ProviderDto.Response;

namespace tbcpay.services.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/xml";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment()
                    ? new BaseResponse
                    {
                        Comment = e.Message,
                        Result = ProviderStatusCodes.GenericError
                    }
                    : new BaseResponse
                    {
                        Comment = "Internal Server Error",
                        Result = ProviderStatusCodes.GenericError
                    };

                _logger.LogInformation("{Exception}",e.Message);
                var xsSubmit = new XmlSerializer(typeof(BaseResponse));
                string xml;

                 using (var sww = new StringWriter())
                 {
                     using (var writer = XmlWriter.Create(sww))
                     {
                         xsSubmit.Serialize(writer, response);
                         xml = sww.ToString();
                     }
                 }

                 await context.Response.WriteAsync(xml);
            }
        }
    }
}