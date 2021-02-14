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
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IProductRepository productRepository;

        public ProductsController(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(this.productRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return NotFound();
                product.CreationDate = DateTime.Now;
                this.productRepository.Add(product);
                return Ok("Product successfully registered");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}