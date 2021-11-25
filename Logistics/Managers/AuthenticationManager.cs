using Contracts;
using Entities.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;

        public AuthenticationManager(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<AuthenticatedUserInfo> Authenticate(string login, string password)
        {
            try
            {
                var userInfo = await TryToAuthenticate(login, password);
                return userInfo;
            }
            catch (Exception e)
            {
                logger.LogInfo(e.Message);
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private async Task<AuthenticatedUserInfo> TryToAuthenticate(string login, string password)
        {
            var userId = await repository.Users.GetUserIdByLoginIfExistAsync(login);
            var userHash = await repository.Users.GetPasswordHashByIdAsync(userId);

            var passwordHash = AuthenticationUtility.CalculateStringHash(password);
            if (passwordHash != userHash)
                throw new Exception("Wrong password");

            var userRole = await repository.Users.GetRoleById(userId);

            var userInfo = new AuthenticatedUserInfo
            {
                Role = userRole,
                userId = userId
            };

            return userInfo;
        }
    }
}
