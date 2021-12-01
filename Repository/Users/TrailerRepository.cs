using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class TrailerRepository : RepositoryBase<Trailer>, ITrailerRepository
    {
        public TrailerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateTrailer(Trailer trailer)
            => Create(trailer);

        public void DeleteTrailer(Trailer trailer)
            => Delete(trailer);

        public async Task<IEnumerable<Trailer>> GetAllTrailersAsync(bool trackChanges)
            => await FindAll(trackChanges)
            .ToListAsync();

        public async Task<Trailer> GetTrailerByIdAsync(Guid id, bool trackChanges)
            => await FindByCondition(Trailer => Trailer.Id == id, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<Trailer> GetTrailerByRegistrationNumberAsync(string number, bool trackChanges)
            => await FindByCondition(Trailer => Trailer.RegistrationNumber == number, trackChanges)
            .SingleOrDefaultAsync();
    }
}
