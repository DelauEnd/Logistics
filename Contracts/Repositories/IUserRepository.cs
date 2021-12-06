using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserRepository
    {
        public Task<Guid> GetUserIdByLoginIfExistAsync(string login);
        public Task<string> GetPasswordHashByIdAsync(Guid id);
        public Task<UserRole> GetRoleByIdAsync(Guid userGuid);
        public Task<User> GetUserByIdAsync(Guid id, bool trackChanges);
        public Task<IEnumerable<User>> GetAllUsersAsync(bool trackChanges);
        void CreateUser(User user);
        void UpdateUser(User user);
    }
}
