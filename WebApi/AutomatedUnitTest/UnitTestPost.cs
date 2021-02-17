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
        //[Fact]
        //public async void Task_PostOrder_Return_OkResult()
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

        //    var orderController = new OrdersController(orderService);

        //    //Act
        //    var orderResquest = new OrderRequest();
        //    orderResquest.UserId = 1;
        //    orderResquest.ProductList = new List<ProductRequest>
        //    {
        //        new ProductRequest {ProductId = 1, Price = 1, Quantity = 1},
        //        new ProductRequest {ProductId = 2, Price = 2, Quantity = 2}
        //    };
        //    var data = await orderController.Post(1, orderResquest);

        //    //Assert
        //    Assert.IsType<OkObjectResult>(data);
        //}

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