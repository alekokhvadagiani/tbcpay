using BaseResponse = tbcpay.services.Dto.ProviderDto.Response.BaseResponse;

namespace tbcpay.services.ProviderService.Abstracts
{
    public interface ICheck
    {
        public Task<BaseResponse> CheckCommand(CheckRequest command);
    }
}