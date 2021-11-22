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
    }
}
