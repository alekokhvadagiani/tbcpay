namespace tbcpay.services.ProviderService.Abstracts;

public interface ICheck
{
    public Task<BaseResponse> CheckCommand(CheckRequest command);
}