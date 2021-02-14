using System.Collections.Generic;
using System.Threading.Tasks;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;

namespace DOMAIN.Services
{
    public class OrderContainsProductService
    {
        private readonly IOrderContainsProductRepository orderContainsProductRepository;

        public OrderContainsProductService(IOrderContainsProductRepository orderContainsProductRepository)
        {
            this.orderContainsProductRepository = orderContainsProductRepository;
        }

        public async Task<IEnumerable<OrderContainsProduct>> GetAll()
        {
            return await this.orderContainsProductRepository.GetAllAsyn();
        }

        public void Dispose()
        {
            this.orderContainsProductRepository.Dispose();
        }
    }
}