using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATA.Contexts;
using DATA.Repositories.Base;
using DOMAIN.Interfaces.Repositories;
using DOMAIN.Models;
using DOMAIN.ODTClasses;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderResponse>> GetFullOrders()
        {
            var orders = await this._context.Set<Order>()
                .Include(item => item.OrderContainsProducts)
                .ThenInclude(it => it.Product)
                .ToArrayAsync();

            var ordersResponse = new List<OrderResponse>();
            foreach (var order in orders)
            {
                var orderResponse = new OrderResponse();
                orderResponse.OrderId = order.Id;
                orderResponse.TotalAmount = order.TotalAmount;
                orderResponse.UserId = order.UserId;
                var productsResponse = new List<ProductResponse>();
                foreach (var ocp in order.OrderContainsProducts.ToList())
                {
                    var productResponse = new ProductResponse();
                    productResponse.ProductName = ocp.Product.Name;
                    productResponse.ProductId = ocp.ProductId;
                    productResponse.Quantity = ocp.Quantity;
                    productResponse.Price = ocp.Product.Price;
                    productsResponse.Add(productResponse);
                }

                orderResponse.Products = productsResponse;
                ordersResponse.Add(orderResponse);
            }

            return ordersResponse;
        }
    }
}