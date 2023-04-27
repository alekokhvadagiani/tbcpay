namespace tbcpay.services.ProviderService;

public class Check : ICheck
{
    public Task<BaseResponse> CheckCommand(CheckRequest command) =>
        throw new NotImplementedException();
}