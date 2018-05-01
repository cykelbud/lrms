using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payout.Core.ApplicationServices;
using Payout.Response;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PayoutController : Controller
    {
        private readonly IPayoutService _payoutService;

        public PayoutController(IPayoutService payoutService)
        {
            _payoutService = payoutService;
        }

        [HttpGet()]
        public async Task<IEnumerable<PayoutDto>> GetAll()
        {
            return await _payoutService.GetAll();
        }


        //[HttpPost("")]
        //public async Task SimulateReceivePayout([FromBody] ReceivePayoutRequest request)
        //{
        //    await _payoutService.ReceivePayout(request);
        //}

        //[HttpPost("")]
        //public async Task SimulatePayoutDue([FromBody] PayoutDueRequest request)
        //{
        //    await _PayoutService.PayoutDue(request);
        //}
    }

}
