using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace TbcPay.Contracts.Dto.ProviderDto.Request;

public class BaseRequest
{
    [FromQuery(Name = "command")] public string Command { get; set; }
}

public class BaseRequestValidator : AbstractValidator<BaseRequest>
{
    public BaseRequestValidator()
    {
        RuleFor(a => a.Command).NotEmpty().NotNull();
        RuleFor(a => a.Command).IsInEnum();
    }
}