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
    public class CargoTypeValidator : IValidator<CargoType>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public CargoTypeValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(CargoType entity)
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

        private async void TryToValid(CargoType type)
        {
            if (await CargoTypeAlredyExist(type))
                throw new Exception("Такой тип груза уже существует");
        }

        private async Task<bool> CargoTypeAlredyExist(CargoType type)
        {
            var typeToCheck = await repository.Types.GetTypeByTitleAsync(type.Title, false);
            if(typeToCheck != null)
                return true;
            return false;
        }
    }
}
