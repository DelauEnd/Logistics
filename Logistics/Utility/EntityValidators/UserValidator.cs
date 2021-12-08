using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics.EntityValidators
{
    public class UserValidator : IValidator<User>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public UserValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(User entity)
        {
            try
            {
                TryToValid(entity);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка");
                logger.LogError(e.Message);
                return false;
            }
        }

        private async void TryToValid(User user)
        {
            if (await UserLoginAlredyExist(user))
                throw new Exception("Пользователь с таким логином уже существует");
        }

        private async Task<bool> UserLoginAlredyExist(User user)
        {
            try
            {
                await repository.Users.GetUserIdByLoginIfExistAsync(user.AccountInfo.Login);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
