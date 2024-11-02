using E_Commerce.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services.Contract
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdateIntent(string basketId);
    }
}
