using TbcPay.Contracts.Dto.ProviderDto.Request;
using TbcPay.Contracts.Dto.ProviderDto.Response;

namespace tbcpay.services.ProviderService;

public class Pay : IPay
{
    public Task<BaseResponse> Deposit(PayRequest request)
        => throw new NotImplementedException();
}