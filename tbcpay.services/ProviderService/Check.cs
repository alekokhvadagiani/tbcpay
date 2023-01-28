using System.Threading.Tasks;
using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.Dto.ProviderDto.Response;
using tbcpay.services.ProviderService.Abstracts;

namespace tbcpay.services.ProviderService
{
    public class Check : ICheck
    {
        public Task<BaseResponse> CheckCommand(CheckRequest command) =>
            throw new System.NotImplementedException();
    }
}