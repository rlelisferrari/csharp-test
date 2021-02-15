using System;
using System.Collections.Generic;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace AutomatedUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async void Task_GetUsers_Return_OkResult()
        {
            //Arrange
            var userRepository = new Mock<IUserRepository>();
            var userService = new UserService(userRepository.Object);
            var usersController = new UsersController(userService);

            //Act
            var data = await usersController.Get("", "", "", DateTime.MinValue, DateTime.MaxValue);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<User>>>(data);
        }

        [Fact]
        public async void Task_PostUser_Return_OkResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var productService = new ProductService(productRepository.Object);
            var productsController = new ProductsController(productService);

            //Act
            var data = await productsController.Post(
                new Product
                {
                    Id = 1, Name = "Test", Price = 1.0f, Description = "TesteDescr", CreationDate = DateTime.Now
                });

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }
    }
}