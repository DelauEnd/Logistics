using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class CargoTypeRepository : RepositoryBase<CargoType>, ICargoTypeRepository
    {
        public CargoTypeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateType(CargoType cargoType)
            => Create(cargoType);

        public async Task<IEnumerable<CargoType>> GetAllTypes(bool trackChanges)
            => await FindAll(trackChanges).ToListAsync();

        public async Task<CargoType> GetTypeByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(type => type.Id == id, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<CargoType> GetTypeByTitleAsync(string title, bool trackChanges)
            => await FindByCondition(type => type.Title == title, trackChanges)
            .SingleOrDefaultAsync();

        public void LoadType()
            => Load();

        public void UpdateType(CargoType cargoType)
            => Update(cargoType);
    }
}
