using Entities.Models;
using Entities.RequestFeautures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRouteRepository
    {
        Task<Route> GetRouteByIdAsync(Guid Id, bool trackChanges);
        Task<IEnumerable<Route>> GetAllRoutesAsync(RouteParameters parameters ,bool trackChanges);
        void CreateRoute(Route route);
        void DeleteRoute(Route route);
    }
}
