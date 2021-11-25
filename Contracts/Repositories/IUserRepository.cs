using Entities.Enums;
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
        Task<EUserRole> GetRoleById(Guid userGuid);
    }
}
