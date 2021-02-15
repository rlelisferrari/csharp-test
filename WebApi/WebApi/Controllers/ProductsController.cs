using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get(
            [FromQuery] string name,
            string description,
            DateTime initial,
            DateTime final)
        {
            var products = await this.productService.FindProducts(name, description, initial, final);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await this.productService.Add(product);
            return Ok("Product successfully registered");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product productChanged)
        {
            var product = await this.productService.UpdateProduct(id, productChanged);
            return Ok(product);
        }
    }
}