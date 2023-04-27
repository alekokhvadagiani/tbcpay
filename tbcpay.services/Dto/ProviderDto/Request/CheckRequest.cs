namespace tbcpay.services.Dto.ProviderDto.Request;

public class CheckRequest : BaseRequest
{
    [FromQuery(Name = "account")] 

    public string Account { get; set; }
}


public class CheckValidator : AbstractValidator<CheckRequest>
{
    public CheckValidator()
    {
        RuleFor(a => a.Account).NotEmpty().WithMessage(ProviderStatusCodes.InvalidAccountIdFormat.ToString());
    }
}