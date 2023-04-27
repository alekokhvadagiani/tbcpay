namespace tbcpay.services.Middlewares
{
    public class CommandMiddleware : IMiddleware
    {
        private readonly IPay _pay;
        private readonly ICheck _check;

        public CommandMiddleware(IPay pay, ICheck check)
        {
            _pay = pay;
            _check = check;
        }

        private static async Task WriteResponseAsync(HttpResponse response, BaseResponse responseData)
        {
            response.ContentType = "application/xml";
            response.StatusCode = (int)HttpStatusCode.OK;
            var serializer = new XmlSerializer(typeof(BaseResponse));
            await using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, responseData);
                await response.WriteAsync(writer.ToString());
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string command = context.Request.Query["command"];
            if (!string.IsNullOrEmpty(command))
            {
                switch (command.ToLower())
                {
                    case "check":
                        var checkRequest = DeserializeXml<CheckRequest>(context.Request.Query["request"]);
                        var checkResponse = await _check.CheckCommand(checkRequest);
                        await WriteResponseAsync(context.Response, checkResponse);
                        return;
                    case "pay":
                        var payRequest = DeserializeXml<PayRequest>(context.Request.Query["request"]);
                        var payResponse = await _pay.Deposit(payRequest);
                        await WriteResponseAsync(context.Response, payResponse);
                        return;
                }
            }

            await next(context);
        }

        private static T DeserializeXml<T>(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var reader = new StringReader(xmlString);
            return (T)serializer.Deserialize(reader);
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
