using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext context;
        private readonly IOrderRepository orderRepository;

        public OrdersController(AppDbContext context, IOrderRepository orderRepository)
        {
            this.context = context;
            this.orderRepository = orderRepository;
        }
    }
}