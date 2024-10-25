using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderServices, IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromQuery] OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, E_Commerce.Core.Entities.Order_Aggregate.Address>(orderDto.ShippingAddress);
            var Order = await _orderServices.CreateOrderAsync(orderDto.BuyerEmail,
                orderDto.BasketId,
                orderDto.DeliveryMethodId,
                address);
            if (Order is null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderFromUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrderForUserAsync(buyerEmail);
            return Ok(orders);
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<Order>> GetOrderByIdFromUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrderByIdForUserAsync(id , buyerEmail);
            if(orders is null) return NotFound(new ApiResponse(404));
            return Ok(orders);
        }
    }
}
