using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TbcPay.Contracts.Enums;

namespace TbcPay.Contracts.Dto.ProviderDto.Request;

public class PayRequest : BaseRequest
{
    [FromQuery(Name = "txn_id")] public string TxnId { get; set; }

    [FromQuery(Name = "account")] public string Account { get; set; }

    [FromQuery(Name = "sum")] public int Sum { get; set; }


    public class PayRequestValidator : AbstractValidator<PayRequest>
    {
        public PayRequestValidator()
        {
            RuleFor(a => a.Account).NotEmpty().WithMessage(ProviderStatusCodes.InvalidAccountIdFormat.ToString());
            RuleFor(a => a.TxnId).NotEmpty();
            RuleFor(a => a.Sum).NotEmpty();
        }
    }
}