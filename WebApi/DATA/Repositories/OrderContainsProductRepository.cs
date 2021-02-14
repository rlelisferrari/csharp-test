using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;

namespace DATA.Repositories
{
    public class OrderContainsProductRepository
        : GenericRepository<OrderContainsProduct>, IOrderContainsProductRepository
    {
        public OrderContainsProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}