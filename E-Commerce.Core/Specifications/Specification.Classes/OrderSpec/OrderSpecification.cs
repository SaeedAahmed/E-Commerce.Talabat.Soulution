using E_Commerce.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Specification.Classes.OrderSpec
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string email):base(o=>o.BuyerEmail == email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDesc(o => o.OrderDate);

        }
        public OrderSpecification(string email , int id) : base(o => o.BuyerEmail == email && o.Id==id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }
    }
}
