using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IProductRepository productRepository;

        public ProductsController(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
        }
    }
}