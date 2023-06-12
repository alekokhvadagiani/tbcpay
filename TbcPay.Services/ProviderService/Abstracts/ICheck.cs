using TbcPay.Contracts.Dto.ProviderDto.Request;
using TbcPay.Contracts.Dto.ProviderDto.Response;

namespace tbcpay.services.ProviderService.Abstracts;

public interface ICheck
{
    public Task<BaseResponse> CheckCommand(CheckRequest command);
}