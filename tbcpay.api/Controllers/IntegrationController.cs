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
    [ServiceFilter(typeof(ModelStateFilterAttribute))]

    public class IntegrationController : ControllerBase
    {
        private readonly IPay _pay;
        private readonly ICheck _check;

        public IntegrationController(IPay pay, ICheck check)
        {
            _pay = pay;
            _check = check;
        }
        
        [HttpGet("{commandName?}")]
        public BaseResponse Api(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                return new BaseResponse
                {
                    Comment = "'command' query parameter is required",
                    Result = ProviderStatusCodes.GenericError
                };
            }
            return new BaseResponse
            {
                Comment = $"Unknown command {commandName}",
                Result = ProviderStatusCodes.GenericError
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Check")]
        public async Task<BaseResponse> Check([FromQuery] CheckRequest request) =>
            await _check.CheckCommand(request);

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Pay")]
        public async Task<BaseResponse>  Pay([FromQuery] PayRequest request) => 
           await _pay.Deposit(request);
    }
}