using System;
using System.Collections.Generic;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IOrderRepository orderRepository;

        public OrdersController(AppDbContext context, IOrderRepository orderRepository)
        {
            this.context = context;
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(this.orderRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Post(int userId, List<int> productIds)
        {
            try
            {
                //Adicionar validação de ordem já cadastrada, produto e usuario não cadastrado

                foreach (var productId in productIds)
                {
                    var order = new Order();
                    order.UserId = userId;
                    order.ProductId = productId;
                    this.orderRepository.Add(order);
                }

                return Ok("Order successfully registered");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}