using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DOMAIN.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await this.orderRepository.GetAllAsyn();
        }

        public void Dispose()
        {
            this.orderRepository.Dispose();
        }
    }
}