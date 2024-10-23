using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail ,
            Address shippingAddress , 
            DeliveryMethod deliveryMethod, 
            ICollection<OrderItems> items, 
            decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress= shippingAddress;
            DeliveryMethod= deliveryMethod;
            Items= items;
            SubTotal= subTotal;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } //Navigtional Prop [one]
        public ICollection<OrderItems> Items { get; set; } = new HashSet<OrderItems>();
        public decimal SubTotal { get; set; }
        public decimal Total()
            => DeliveryMethod.Cost + SubTotal;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
