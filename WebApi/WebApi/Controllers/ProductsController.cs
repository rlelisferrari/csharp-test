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
        public ActionResult<IEnumerable<string>> Get([FromQuery] string name, string description, DateTime initial, DateTime final)
        {
            return Ok(
                this.productRepository.FindAll(
                    item => (name == null || item.Name.Contains(name))
                            && (description == null || item.Description.Contains(description))
                            && (initial == DateTime.MinValue
                                || final == DateTime.MinValue
                                || initial <= item.CreationDate
                                && item.CreationDate <= final)));
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