using TbcPay.Contracts.Dto.ProviderDto.Request;
using TbcPay.Contracts.Dto.ProviderDto.Response;

namespace tbcpay.services.ProviderService.Abstracts;

public interface IPay
{
    public Task<BaseResponse> Deposit(PayRequest request);
}