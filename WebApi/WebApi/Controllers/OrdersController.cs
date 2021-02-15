using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.AuxClasses;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService orderService;

        public OrdersController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await this.orderService.GetAll();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int userId, OrderRequest orderRequest)
        {
            await this.orderService.Add(userId, orderRequest);
            return Ok("Order successfully registered");
        }
    }
}