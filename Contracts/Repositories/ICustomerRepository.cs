using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<Customer> GetSenderByOrderIdAsync(Guid id, bool trackChanges);
        Task<Customer> GetDestinationByOrderIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChangess);
        Task<Customer> GetCustomerByIdAsync(Guid id, bool trackChanges);
        void CreateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
