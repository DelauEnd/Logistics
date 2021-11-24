using Entities.Models;
using Entities.RequestFeautures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(Guid Id, bool trackChanges);
        Task<IEnumerable<Order>> GetAllOrdersAsync(OrderParameters parameters, bool trackChanges);
        void CreateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
