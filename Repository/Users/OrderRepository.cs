using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeautures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateOrder(Order order)
            => Create(order);

        public void DeleteOrder(Order order)
            => Delete(order);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(OrderParameters parameters, bool trackChanges)
            => await FindAll(trackChanges)
            .Include(route => route.Destination)
            .Include(route => route.Sender)
            .Search(parameters.Search)
            .ApplyFilters(parameters)
            .ToListAsync();

        public async Task<Order> GetOrderByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(order => order.Id == id, trackChanges)
            .Include(order => order.Cargoes).ThenInclude(cargo => cargo.Category)
            .Include(route => route.Destination).Include(route => route.Sender)
            .SingleOrDefaultAsync();
    }
}
