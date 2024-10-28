using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities.Order_Aggregate;

namespace E_Commerce.APIs.Helpers
{
    public class OrderPictureUrlResolver : IValueResolver<OrderItems,OrderItemsDto, string>
    {
        private readonly IConfiguration _config;

        public OrderPictureUrlResolver(IConfiguration config) 
        {
            _config = config;
        }
        public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_config["ApiBaseUrl"]}/{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
