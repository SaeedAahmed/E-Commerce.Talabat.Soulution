using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.APIs.Helpers
    .Profiles
{
    public class mappingProfile : Profile
    {
        public mappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D=>D.PictureUrl , O=>O.MapFrom<ProductPicUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemsDto, BasketItems>();
            CreateMap<E_Commerce.Core.Entities.Identity.Address, E_Commerce.APIs.Dtos.AddressDto>();
            CreateMap<E_Commerce.APIs.Dtos.AddressDto, E_Commerce.Core.Entities.Order_Aggregate.Address>();

            CreateMap<Order, OrderToReturnOrderDto>()
              .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
              .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
              .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<OrderItems,OrderItemsDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.ProductUrl, o => o.MapFrom<OrderPictureUrlResolver>());

        }
    }
}
