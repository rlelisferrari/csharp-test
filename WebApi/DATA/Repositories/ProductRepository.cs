using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;

namespace DATA.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}