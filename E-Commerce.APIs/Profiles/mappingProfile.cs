using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Profiles
{
    public class mappingProfile : Profile
    {
        public mappingProfile() 
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D=>D.ProductBrand,O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(D=>D.ProductType , O=>O.MapFrom(S=>S.ProductType.Name));
        }
    }
}
