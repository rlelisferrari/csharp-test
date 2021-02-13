﻿using System.Collections.Generic;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories;
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
    }
}