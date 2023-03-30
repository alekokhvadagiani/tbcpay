using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using tbcpay.services.Dto.ProviderDto.Response;
using tbcpay.services.Helpers;

namespace tbcpay.services.ServiceFilters
{
    public class ModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState.IsValid;
           
            if (!modelState)
            {
                 ProviderStatusCodes code; 
                 string message;
                 
                 var error = context.ModelState.SelectMany(x => x.Value.Errors).First();

                if (error.ErrorMessage==ProviderStatusCodes.InvalidAccountIdFormat.ToString())
                {
                    message = "Invalid Account Id Format";
                    code = ProviderStatusCodes.InvalidAccountIdFormat;
                }
                else if (!string.IsNullOrEmpty(error.ErrorMessage))
                {
                    message = error.ErrorMessage;
                    code = ProviderStatusCodes.GenericError;
                }
                else if (error.Exception?.Message != null)
                {
                    message = error.Exception.Message;
                    code = ProviderStatusCodes.GenericError;
                }
                else
                {
                    message = "Validation error happened";
                    code = ProviderStatusCodes.GenericError;
                }
                
                context.Result = new BadRequestObjectResult(new BaseResponse
                {
                    Result = code,
                    Comment = message
                });
            }

            base.OnActionExecuting(context);
        }
    }
}