﻿using System;
using System.Collections.Generic;
using DATA.Contexts;
using DATA.Repositories;
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
        private readonly IOrderContainsProductRepository orderContainsProductRepository;

        public OrdersController(AppDbContext context, IOrderRepository orderRepository, IOrderContainsProductRepository orderContainsProductRepository)
        {
            this.context = context;
            this.orderRepository = orderRepository;
            this.orderContainsProductRepository = orderContainsProductRepository;
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

                var order = new Order() { UserId = userId };
                this.orderRepository.Add(order);
                foreach (var productId in productIds)
                {
                    var orderContainsProduct = new OrderContainsProduct();
                    orderContainsProduct.OrderId = order.Id;
                    orderContainsProduct.ProductId = productId;
                    orderContainsProduct.Quantity = 1;
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