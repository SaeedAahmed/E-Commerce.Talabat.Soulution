using E_Commerce.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes.OrderSpec
{
    public class OrderWithPaymentIntentIdSpec:BaseSpecification<Order>
    {
        public OrderWithPaymentIntentIdSpec(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId) { }
    }
}
