using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Services.Contract;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Service
{
    public class PaymentService : IPaymentServices
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IConfiguration config,IUnitOfWork unitOfWork,IBasketRepository basketRepository)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket> CreateOrUpdateIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:Secretkey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                basket.ShippngCost = deliveryMethod.Cost;
                shippingPrice = deliveryMethod.Cost;
            }

            if(basket?.Items?.Count() > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<E_Commerce.Core.Entities.Products.Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price
;               }
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100,
                    Currency = "Usd",
                    PaymentMethodTypes = new List<string>() { "Card"}
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shippingPrice * 100
                };
                await service.UpdateAsync(basket.PaymentId , options);
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
