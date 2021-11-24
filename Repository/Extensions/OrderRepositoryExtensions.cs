using Entities.Models;
using Entities.RequestFeautures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class OrderRepositoryExtensions
    {
        public static IQueryable<Order> ApplyFilters(this IQueryable<Order> orders, OrderParameters parameters)
        {
            return null;
        }

        public static IQueryable<Order> Search(this IQueryable<Order> orders, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return orders;

            var searchValues = search.Trim().ToLower();

            return orders.Where(orders =>
                   orders.Destination.Address.Contains(searchValues) ||
                   orders.Sender.Address.Contains(searchValues) ||
                   orders.Id.ToString().Contains(searchValues));
        }
    }
}
