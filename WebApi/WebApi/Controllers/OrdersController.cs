using System;
using System.Collections.Generic;
using System.Linq;
using DATA.Repositories;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderContainsProductRepository orderContainsProductRepository;

        public OrdersController(
            IOrderRepository orderRepository,
            IOrderContainsProductRepository orderContainsProductRepository)
        {
            this.orderRepository = orderRepository;
            this.orderContainsProductRepository = orderContainsProductRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(this.orderRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Post(int userId, List<OrderContainsProduct> ocp)
        {
            try
            {
                //Adicionar validação de ordem já cadastrada, produto e usuario não cadastrado

                var cost = ocp.Sum(item => item.Quantity * item.Product.Price);
                var order = new Order {UserId = userId, TotalAmount = cost};
                this.orderRepository.Add(order);
                foreach (var item in ocp)
                {
                    var orderContainsProduct = new OrderContainsProduct();
                    orderContainsProduct.OrderId = order.Id;
                    orderContainsProduct.ProductId = item.ProductId;
                    orderContainsProduct.Quantity = item.Quantity;
                    this.orderContainsProductRepository.Add(orderContainsProduct);
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