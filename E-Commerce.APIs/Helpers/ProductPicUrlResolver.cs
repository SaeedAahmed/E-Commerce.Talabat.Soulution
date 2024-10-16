using AutoMapper;
using E_Commerce.APIs.Dtos;
using E_Commerce.Core.Entities.Products;

namespace E_Commerce.APIs.Helpers
{
    public class ProductPicUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPicUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
        }
    }
}
