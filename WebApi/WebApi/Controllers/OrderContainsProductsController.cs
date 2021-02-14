using System.Collections.Generic;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderContainsProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IOrderContainsProductRepository orderContainsProductRepository;

        public OrderContainsProductsController(
            AppDbContext context,
            IOrderContainsProductRepository orderContainsProductRepository,
            IOrderRepository orderRepository)
        {
            this.context = context;
            this.orderContainsProductRepository = orderContainsProductRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(this.orderContainsProductRepository.GetAll());
        }
    }
}