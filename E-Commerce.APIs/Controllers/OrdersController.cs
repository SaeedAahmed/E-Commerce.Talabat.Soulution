using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.APIs.Errors;
using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var Order = await _orderServices.CreateOrderAsync(orderDto.BuyerEmail,
                orderDto.BasketId,
                orderDto.DeliveryMethodId,
                address);
            if (Order is null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
    }
}
