using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.ODTClasses;

namespace DOMAIN.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderContainsProductRepository orderContainsProductRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderContainsProductRepository orderContainsProductRepository,
            IUserRepository userRepository,
            IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.orderContainsProductRepository = orderContainsProductRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderResponse>> GetAll()
        {
            return await this.orderRepository.GetFullOrders();
        }

        public async Task Add(int userId, OrderRequest orderRequest)
        {
            var user = await this.userRepository.GetAsync(userId);
            if (user == null)
                throw new InvalidOperationException($"User with Id={userId} not found");

            foreach (var productRequest in orderRequest.ProductList)
            {
                var product = await this.productRepository.GetAsync(productRequest.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with Id={productRequest.ProductId} not found");
            }

            var cost = orderRequest.ProductList.Sum(item => item.Quantity * item.Price);
            var order = new Order {UserId = userId, TotalAmount = cost};
            await this.orderRepository.AddAsyn(order);
            foreach (var productRequest in orderRequest.ProductList)
            {
                var orderContainsProduct = new OrderContainsProduct();
                orderContainsProduct.OrderId = order.Id;
                orderContainsProduct.ProductId = productRequest.ProductId;
                orderContainsProduct.Quantity = productRequest.Quantity;
                await this.orderContainsProductRepository.AddAsyn(orderContainsProduct);
            }
        }

        public void Dispose()
        {
            this.orderRepository.Dispose();
        }
    }
}