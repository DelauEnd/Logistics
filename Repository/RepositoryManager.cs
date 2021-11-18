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
        private ITransportRepository transportRepository;
        private IUserRepository userRepository;

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

        public ITransportRepository Transport
        {
            get
            {
                if (transportRepository == null)
                    transportRepository = new TransportRepository(repositoryContext);
                return transportRepository;
            }
        }

        public void ClearTrackers()
            => repositoryContext.ChangeTracker.Clear();

        public async Task SaveAsync() 
            => await repositoryContext.SaveChangesAsync();

    }
}
