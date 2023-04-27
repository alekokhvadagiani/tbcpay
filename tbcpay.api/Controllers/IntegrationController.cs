using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tbcpay.services.Dto.ProviderDto.Request;
using tbcpay.services.Dto.ProviderDto.Response;
using tbcpay.services.Helpers;
using tbcpay.services.ProviderService.Abstracts;
using tbcpay.services.ServiceFilters;

namespace tbcpay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ModelStateFilter))]

    public class IntegrationController : ControllerBase
    {
        private readonly IPay _pay;
        private readonly ICheck _check;

        public IntegrationController(IPay pay, ICheck check)
        {
            _pay = pay;
            _check = check;
        }

        [HttpGet("{commandName}")]
        public async Task<BaseResponse> Api(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "check":
                    return await _check.CheckCommand(Request.Query);
                case "pay":
                    return await _pay.Deposit(Request.Query);
                default:
                    return new BaseResponse
                    {
                        Comment = $"Unknown command {commandName}",
                        Result = ProviderStatusCodes.GenericError
                    };
            }
        }
    }
}
