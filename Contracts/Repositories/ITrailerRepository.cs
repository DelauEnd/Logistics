using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITrailerRepository
    {
        Task<IEnumerable<Trailer>> GetAllTrailersAsync(bool trackChanges);
        Task<Trailer> GetTrailerByIdAsync(Guid id, bool trackChanges);
        Task<Trailer> GetTrailerByRegistrationNumberAsync(string number, bool trackChanges);
        void CreateTrailer(Trailer trailer);
        void DeleteTrailer(Trailer trailer);
        void UpdateTrailer(Trailer trailer);
    }
}
