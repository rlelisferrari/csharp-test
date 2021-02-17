using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.ODTClasses;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// returns registered orders 
        /// </summary>
        /// <response code="200">returns registered orders</response>
        /// <response code="401">Unauthorized</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderResponse>), 200)]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> Get()
        {
            var orders = await this.orderService.GetAll();
            return Ok(orders);
        }

        /// <summary>
        /// register a new order 
        /// </summary>
        /// <response code="200">Product successfully registered</response>
        /// <response code="500">Invalid parameters</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        public async Task<ActionResult> Post(int userId, OrderRequest orderRequest)
        {
            await this.orderService.Add(userId, orderRequest);
            return Ok("Order successfully registered");
        }
    }
}