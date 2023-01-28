using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.ProviderService.Abstracts;

namespace tbcpay.services.ProviderService
{
    public class Pay : IPay
    {
        public string Deposit(PayRequest request) =>
            throw new System.NotImplementedException();
    }
}