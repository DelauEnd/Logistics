using Contracts;
using Contracts.Repositories;
using Entities;
using Repository.Users;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private ICargoCategoryRepository cargoCategoryRepository;
        private ICargoRepository cargoRepository;
        private ICustomerRepository customerRepository;
        private IOrderRepository orderRepository;
        private IRouteRepository routeRepository;
        private ITruckRepository truckRepository;
        private IUserRepository userRepository;
        private ITrailerRepository trailerRepository;
        private ICargoTypeRepository cargoTypeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public ICargoCategoryRepository CargoCategories
        {
            get
            {
                if (cargoCategoryRepository == null)
                    cargoCategoryRepository = new CargoCategoryRepository(repositoryContext);
                return cargoCategoryRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(repositoryContext);
                return userRepository;
            }
        }

        public ICargoRepository Cargoes
        {
            get
            {
                if (cargoRepository == null)
                    cargoRepository = new CargoRepository(repositoryContext);
                return cargoRepository;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(repositoryContext);
                return customerRepository;
            }
        }
        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(repositoryContext);
                return orderRepository;
            }
        }
        public IRouteRepository Routes
        {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(repositoryContext);
                return routeRepository;
            }
        }

        public ITruckRepository Trucks
        {
            get
            {
                if (truckRepository == null)
                    truckRepository = new TruckRepository(repositoryContext);
                return truckRepository;
            }
        }

        public ICargoTypeRepository Types
        {
            get
            {
                if (cargoTypeRepository == null)
                    cargoTypeRepository = new CargoTypeRepository(repositoryContext);
                return cargoTypeRepository;
            }
        }

        public ITrailerRepository Trailers
        {
            get
            {
                if (trailerRepository == null)
                    trailerRepository = new TrailerRepository(repositoryContext);
                return trailerRepository;
            }
        }

        public void ClearTrackers()
            => repositoryContext.ChangeTracker.Clear();

        public async Task SaveAsync()
        {
            await repositoryContext.SaveChangesAsync();
            await RefreshAllAsync();
        }

        private async Task RefreshAllAsync()
        {
            foreach (var entity in repositoryContext.ChangeTracker.Entries())
            {
                await entity.ReloadAsync();
            }
        }
    }
}
