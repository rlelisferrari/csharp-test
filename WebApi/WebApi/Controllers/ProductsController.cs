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

        /// <summary>
        /// returns registered products 
        /// </summary>
        /// <response code="200">returns registered products</response>
        /// <response code="401">Unauthorized</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), 200)]
        public async Task<ActionResult<IEnumerable<Product>>> Get(
            [FromQuery] string name,
            string description,
            DateTime initial,
            DateTime final)
        {
            var products = await this.productService.FindProducts(name, description, initial, final);
            return Ok(products);
        }

        /// <summary>
        /// register a new product 
        /// </summary>
        /// <response code="200">Product successfully registered</response>
        /// <response code="500">Invalid parameters</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            await this.productService.Add(product);
            return Ok("Product successfully registered");
        }

        /// <summary>
        /// updated product registration 
        /// </summary>
        /// <response code="200">returns the updated product</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut("{id}")]
        //[ProducesResponseType(typeof(Product), 200)]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] Product productChanged)
        {
            var product = await this.productService.UpdateProduct(id, productChanged);
            return Ok(product);
        }
    }
}