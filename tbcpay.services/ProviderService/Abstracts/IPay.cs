namespace tbcpay.services.ProviderService.Abstracts;

public interface IPay
{
    public Task<BaseResponse> Deposit(PayRequest request);
}