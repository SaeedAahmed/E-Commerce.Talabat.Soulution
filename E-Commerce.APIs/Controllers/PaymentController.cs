using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
using E_Commerce.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentController( IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateIntent(string id)
        {
            var basket = await _paymentServices.CreateOrUpdateIntent(id);
            if (basket == null) return BadRequest(new ApiResponse(400));
            return Ok(basket);
        }

    }
}
