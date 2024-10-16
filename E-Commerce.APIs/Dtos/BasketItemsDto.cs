using System.ComponentModel.DataAnnotations;

namespace E_Commerce.APIs.Dtos
{
    public class BasketItemsDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue ,ErrorMessage ="price must be grater than zero")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a quantity of at least 1.")]
        public int Quantity { get; set; }
    }
}