using E_Commerce.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string buyerEmail,
            string basketId,
            int deliveryMethodId,
            Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdForUserAsync(int orderId ,string buyerEmail);

    }
}
