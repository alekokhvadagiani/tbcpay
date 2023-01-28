using System.Threading.Tasks;
using BaseResponse = tbcpay.services.Dto.ProviderDto.Response.BaseResponse;
using tbcpay.services.Dto.ProviderDto.Request;

namespace tbcpay.services.ProviderService.Abstracts
{
    public interface ICheck
    {
        public Task<BaseResponse> CheckCommand(CheckRequest command);
    }
}