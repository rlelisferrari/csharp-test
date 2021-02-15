using System;
using System.Threading.Tasks;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;

namespace DATA.Repositories
{
    public class OrderContainsProductRepository : IOrderContainsProductRepository
    {
        private readonly AppDbContext context;

        public OrderContainsProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        #region Implementation of IOrderContainsProductRepository

        public async Task<OrderContainsProduct> AddAsyn(OrderContainsProduct orderContainsProduct)
        {
            this.context.Set<OrderContainsProduct>().Add(orderContainsProduct);
            await this.context.SaveChangesAsync();
            return orderContainsProduct;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}