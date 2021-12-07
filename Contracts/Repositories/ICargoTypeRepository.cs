using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICargoTypeRepository
    {
        Task<IEnumerable<CargoType>> GetAllTypes(bool trackChanges);
        void LoadType();
        Task<CargoType> GetTypeByIdAsync(Guid id, bool trackChanges);
        Task<CargoType> GetTypeByTitleAsync(string title, bool trackChanges);
        void CreateType(CargoType type);
        void UpdateType(CargoType type);
        void DeleteType(CargoType type);
    }
}
