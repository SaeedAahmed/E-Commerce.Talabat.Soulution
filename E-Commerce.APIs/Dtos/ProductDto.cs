using E_Commerce.Core.Entities;

namespace E_Commerce.APIs.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public string ProductBrand { get; set; }
        public int TypeId { get; set; }
        public string ProductType { get; set; }
    }
}
