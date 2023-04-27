namespace tbcpay.services.ServiceFilters;

public class ModelStateFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var error = context.ModelState.SelectMany(x => x.Value.Errors).FirstOrDefault();

            var message = error?.ErrorMessage ?? "Validation error happened";
            var code = error?.ErrorMessage == ProviderStatusCodes.InvalidAccountIdFormat.ToString()
                ? ProviderStatusCodes.InvalidAccountIdFormat
                : ProviderStatusCodes.GenericError;

            context.Result = new BadRequestObjectResult(new BaseResponse
            {
                Result = code,
                Comment = message
            });
        }

        base.OnActionExecuting(context);
    }
}