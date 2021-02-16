using System.Collections.Generic;
using System.Threading.Tasks;
using DOMAIN.Interfaces.Repositories.Base;
using DOMAIN.Models;
using DOMAIN.ODTClasses;

namespace DOMAIN.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<OrderResponse>> GetFullOrders();
    }
}