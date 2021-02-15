using System;
using System.Collections.Generic;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(
            [FromQuery] string name,
            string description,
            DateTime initial,
            DateTime final)
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

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product productChanged)
        {
            var product = this.productRepository.Get(id);

            if (product == null)
                return NotFound();

            product.Name = string.IsNullOrEmpty(productChanged.Name) ? product.Name : productChanged.Name;
            product.Description = string.IsNullOrEmpty(productChanged.Description)
                ? product.Description
                : productChanged.Description;
            product.Price = productChanged.Price <= 0 ? product.Price : productChanged.Price;

            this.productRepository.Update(product, product.Id);

            return Ok();
        }
    }
}