using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace tbcpay.services.Dto.ProviderDto.Request
{
    public class BaseRequest
    {
        [FromQuery(Name = "command")] 
        public Commands Command { get; set; }
    }

    public enum Commands
    {
        Pay,
        Check
    }


    public class BaseRequestValidator : AbstractValidator<BaseRequest>
    {
        public BaseRequestValidator()
        {
            RuleFor(a => a.Command).NotEmpty().NotNull();
            RuleFor(a => a.Command).IsInEnum();
        }
    }
}