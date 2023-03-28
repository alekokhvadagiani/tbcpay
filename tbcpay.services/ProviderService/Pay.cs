using System;
using System.Threading.Tasks;
using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.Dto.ProviderDto.Response;
using tbcpay.services.ProviderService.Abstracts;

namespace tbcpay.services.ProviderService
{
    public class Pay : IPay
    {
        public Task<BaseResponse> Deposit(PayRequest request)
           => throw new NotImplementedException();
    }
}