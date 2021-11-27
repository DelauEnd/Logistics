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
    public static class RouteRepositoryExtensions
    {
        public static IQueryable<Route> ApplyFilters(this IQueryable<Route> routes, RouteParameters parameters)
        {
            return routes;
        }

        public static IQueryable<Route> Search(this IQueryable<Route> route, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return route;

            var searchValues = search.Trim().ToLower();

            return route.Where(route =>
                   route.Trailer.RegistrationNumber.Contains(searchValues) ||
                   route.Truck.RegistrationNumber.Contains(searchValues) ||
                   route.Id.ToString().Contains(searchValues));
        }
    }
}
