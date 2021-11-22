﻿using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Route> GetRouteByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(route => route.Id == id, trackChanges)
            .Include(route => route.Cargoes).ThenInclude(cargo => cargo.Category)
            .Include(route => route.Truck)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesAsync(bool trackChanges)
            => await FindAll(trackChanges)
            .Include(route => route.Truck)
            .ToListAsync();

        public void CreateRoute(Route route)
            => Create(route);

        public void DeleteRoute(Route route)
            => Delete(route);
    }
}
