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
        public async Task<ActionResult<OrderToReturnOrderDto>> CreateOrder([FromQuery] OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, E_Commerce.Core.Entities.Order_Aggregate.Address>(orderDto.ShippingAddress);
            var Order = await _orderServices.CreateOrderAsync(orderDto.BuyerEmail,
                orderDto.BasketId,
                orderDto.DeliveryMethodId,
                address);
            if (Order is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnOrderDto>(Order));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnOrderDto>>> GetOrderFromUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrderForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnOrderDto>>(orders));
        }
        [Authorize]
        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturnOrderDto>> GetOrderByIdFromUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderServices.GetOrderByIdForUserAsync(id , buyerEmail);
            if(orders is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order, OrderToReturnOrderDto>(orders));
        }

        [Authorize]
        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<OrderToReturnOrderDto>> GetDeliveryMethod()
        {
            var deliveryMethod = await _orderServices.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);
        }
    }
}
