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

        [HttpPost("Check")]
        public async Task<BaseResponse> Check([FromBody] CheckRequest request) =>
            await _check.CheckCommand(request);

        [HttpPost("Pay")]
        public async Task<BaseResponse> Pay([FromBody] PayRequest request) =>
           await _pay.Deposit(request);
    }
}
