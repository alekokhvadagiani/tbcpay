using System.Threading.Tasks;
using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.Dto.ProviderDto.Response;

namespace tbcpay.services.ProviderService.Abstracts;

public interface IPay
{
    public Task<BaseResponse> Deposit(PayRequest request);
}