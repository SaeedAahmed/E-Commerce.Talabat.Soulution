using E_Commerce.Core.Entities.Basket;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemsDto> Items { get; set; }

    }
}
