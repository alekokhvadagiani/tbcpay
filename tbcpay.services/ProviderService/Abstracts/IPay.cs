using tbcpay.services.Dto.ProviderDto.Request;

namespace tbcpay.services.ProviderService.Abstracts
{
    public interface IPay
    {
        public string Deposit(PayRequest request);
    }
}