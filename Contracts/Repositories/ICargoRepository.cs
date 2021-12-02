using Entities.Models;
using Entities.RequestFeautures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetCargoesByOrderIdAsync(Guid id, CargoParameters parameters, bool trackChanges);
        Task<IEnumerable<Cargo>> GetCargoesByRouteIdAsync(Guid? id, CargoParameters parameters, bool trackChanges);
        void CreateCargo(Cargo cargo);
        Task MarkTheCargoToRouteAsync(Guid cargoId, Guid routeId);
        Task<Cargo> GetCargoByIdAsync(Guid cargoId, bool trackChanges);
        void DeleteCargo(Cargo cargo);
        void UpdateCargo(Cargo cargo);
        Task<IEnumerable<Cargo>> GetAllCargoesAsync(CargoParameters parameters, bool trackChanges);
    }
}
