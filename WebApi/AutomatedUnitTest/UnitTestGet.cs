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
    public class UnitTestGet
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
        public async void Task_GetProduct_Return_OkResult()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var productService = new ProductService(productRepository.Object);
            var productsController = new ProductsController(productService);

            //Act
            var data = await productsController.Get("", "", DateTime.MinValue, DateTime.MaxValue);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<Product>>>(data);
        }

        //[Fact]
        //public async void Task_PutProduct_Return_OkResult()
        //{
        //    //Arrange
        //    var productRepository = new Mock<IProductRepository>();
        //    var productService = new ProductService(productRepository.Object);
        //    var productsController = new ProductsController(productService);
        //    var product = new Product();
        //    product.Id = 1;
        //    product.Name = "Name";
        //    product.Price = 2f;
        //    product.Description = "Descr";
        //    product.CreationDate = DateAndTime.Now;
        //    //Act
        //    var data = await productsController.Put(1, product);

        //    //Assert
        //    Assert.IsType<ActionResult<IEnumerable<Product>>>(data);
        //}

        //[Fact]
        //public async void Task_GetOrder_Return_OkResult()
        //{
        //    //Arrange
        //    var orderRepository = new Mock<IOrderRepository>();
        //    var orderContainsProductRepository = new Mock<IOrderContainsProductRepository>();
        //    var userRepository = new Mock<IUserRepository>();
        //    var productRepository = new Mock<IProductRepository>();
        //    var orderService = new OrderService(
        //        orderRepository.Object,
        //        orderContainsProductRepository.Object,
        //        userRepository.Object,
        //        productRepository.Object);

        //    //Act
        //    var data = await orderService.GetAll();

        //    //Assert
        //    Assert.IsType<ActionResult<IEnumerable<OrderResponse>>>(data);
        //}
    }
}