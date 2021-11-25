﻿using Contracts;
using Contracts.Repositories;
using Entities;
using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<string> GetPasswordHashByIdAsync(Guid id)
        { 
            var user = await FindByCondition(user => user.Id == id, false)
            .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            return user.AccountInfo.PasswordHashString;
        }

        public async Task<EUserRole> GetRoleById(Guid id)
        {
            var user = await FindByCondition(user => user.Id == id, false)
            .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            return user.Role;
        }

        public async Task<Guid> GetUserIdByLoginIfExistAsync(string login)
        {
            var user = await FindByCondition(user => user.AccountInfo.Login == login, false)
            .FirstOrDefaultAsync();

            if (user == null)
                throw new Exception("User not found");

            return user.Id;
        }
    }
}
