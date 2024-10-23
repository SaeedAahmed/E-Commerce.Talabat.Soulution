using E_Commerce.Core.Entities.Order_Aggregate;
using E_Commerce.Core.Entities.Products;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Core.Services.Contract;
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

        public OrderService(IBasketRepository basketRepo,IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
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
            var order = new Order(buyerEmail , shippingAddress , deliveryMethods , orderItems , subTotal);

            await _unitOfWork.Repository<Order>().Add(order);

            // Save to db
            var res = await _unitOfWork.Complete();
            if (res <= 0) return null;
            return order;

        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
