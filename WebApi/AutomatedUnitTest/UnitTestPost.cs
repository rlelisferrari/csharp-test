using System;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace AutomatedUnitTest
{
    public class UnitTestPost
    {
        [Fact]
        public async void Task_PostProduct_Return_OkResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var productService = new ProductService(productRepository.Object);
            var productsController = new ProductsController(productService);

            //Act
            var data = await productsController.Post(
                new Product
                {
                    Id = 1,
                    Name = "Test",
                    Price = 1.0f,
                    Description = "TesteDescr",
                    CreationDate = DateTime.Now
                });

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
    }
}