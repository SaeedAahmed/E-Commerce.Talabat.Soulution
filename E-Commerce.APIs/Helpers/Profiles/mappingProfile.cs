using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities.Basket;
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
        }
    }
}
