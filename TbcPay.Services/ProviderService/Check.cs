using TbcPay.Contracts.Dto.ProviderDto.Request;
using TbcPay.Contracts.Dto.ProviderDto.Response;

namespace tbcpay.services.ProviderService;

public class Check : ICheck
{
    public Task<BaseResponse> CheckCommand(CheckRequest command) =>
        throw new NotImplementedException();
}