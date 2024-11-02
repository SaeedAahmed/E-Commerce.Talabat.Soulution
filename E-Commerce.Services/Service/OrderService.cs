using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Services.Contract;
using E_Commerce.Core.Specifications.Specification.Classes.OrderSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentServices _paymentServices;

        public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork , IPaymentServices paymentServices)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
            _paymentServices = paymentServices;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // Get Basket From Basket Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            //Get Selected Items at Basket From Product Repo
            var orderItems = new List<OrderItems>();
            if(basket?.Items?.Count > 0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    var productItemOrder = new ProductItemOrder(product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItems(productItemOrder, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            // calculate SubTotal
            var subTotal = orderItems.Sum(item=>item.Price * item.Quantity);

            // Get delivery method from delivery method repo
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            // Create Order
            var spec = new OrderWithPaymentIntentIdSpec(basket.PaymentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            if(existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentServices.CreateOrUpdateIntent(basket.Id);
            }

            var order = new Order(buyerEmail , shippingAddress , deliveryMethods ,basket.PaymentId , orderItems , subTotal );

            await _unitOfWork.Repository<Order>().Add(order);

            // Save to db
            var res = await _unitOfWork.Complete();
            if (res <= 0) return null;
            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethod;
        }

        public async Task<Order> GetOrderByIdForUserAsync(int id, string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail, id);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var Spec = new OrderSpecification(buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return order;
        }
    }
}
