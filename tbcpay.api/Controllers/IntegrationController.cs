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

        [HttpGet]
        public BaseResponse Api() =>
             new()
             {
                Comment = "Invalid Operation",
                Result= ProviderStatusCodes.GenericError
            };


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Check")]
        public Task<BaseResponse> Check([FromQuery] CheckRequest request) =>
            _check.CheckCommand(request);

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Pay")]
        public Task<BaseResponse>  Pay([FromQuery] PayRequest request) => 
            _pay.Deposit(request);
    }
}