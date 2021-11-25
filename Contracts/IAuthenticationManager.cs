using Entities.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        public Task<AuthenticatedUserInfo> Authenticate(string login, string password);
    }
}
