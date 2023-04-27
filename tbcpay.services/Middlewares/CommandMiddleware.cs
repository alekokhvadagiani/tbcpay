namespace tbcpay.services.Middlewares
{
    public class CommandMiddleware : IMiddleware
    {
        private readonly IPay _pay;
        private readonly ICheck _check;

        public CommandMiddleware( IPay pay, ICheck check)
        {
            _pay = pay;
            _check = check;
        }

        private static async Task WriteResponseAsync(HttpResponse response, BaseResponse responseData)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.OK;
            await response.WriteAsync(JsonConvert.SerializeObject(responseData));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string command = context.Request.Query["command"];
            if (!string.IsNullOrEmpty(command))
            {
                switch (command.ToLower())
                {
                    case "check":
                        var checkRequest = JsonConvert.DeserializeObject<CheckRequest>(context.Request.Query["request"]);
                        var checkResponse = await _check.CheckCommand(checkRequest);
                        await WriteResponseAsync(context.Response, checkResponse);
                        return;
                    case "pay":
                        var payRequest = JsonConvert.DeserializeObject<PayRequest>(context.Request.Query["request"]);
                        var payResponse = await _pay.Deposit(payRequest);
                        await WriteResponseAsync(context.Response, payResponse);
                        return;
                }
            }

            await next(context);
        }
    }
}
