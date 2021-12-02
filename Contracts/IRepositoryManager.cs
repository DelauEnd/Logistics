using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICargoTypeRepository Types { get; }
        ICargoRepository Cargoes { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IRouteRepository Routes { get; }
        ITruckRepository Trucks { get; }
        ITrailerRepository Trailers { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
        void ClearTrackers();
    }
}
