using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Order_Aggregate
{
    public class OrderItems : BaseEntity
    {
        public OrderItems()
        {
            
        }
        public OrderItems(ProductItemOrder product , decimal price , int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public ProductItemOrder Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
