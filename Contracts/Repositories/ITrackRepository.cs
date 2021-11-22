using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITruckRepository
    {
        Task<IEnumerable<Truck>> GetAllTrucksAsync(bool trackChanges);
        Task<Truck> GetTruckByIdAsync(Guid id, bool trackChanges);
        Task<Truck> GetTruckByRegistrationNumberAsync(string number, bool trackChanges);
        void CreateTruck(Truck truck);
        void DeleteTruck(Truck truck);
    }
}
