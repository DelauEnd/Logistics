using Contracts;
using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
