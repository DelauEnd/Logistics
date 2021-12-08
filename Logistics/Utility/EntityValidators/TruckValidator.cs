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
    public class TruckValidator : IValidator<Truck>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public TruckValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(Truck entity)
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

        private async void TryToValid(Truck truck)
        {
            if (await RegNumberAlredyExist(truck))
                throw new Exception("Транспорт с таким номером уже существует");
        }

        private async Task<bool> RegNumberAlredyExist(Truck truck)
        {
            var truckToCheck = await repository.Trucks.GetTruckByRegistrationNumberAsync(truck.RegistrationNumber, false);  
            if (truckToCheck != null)
                return true;

            var trailerToCheck = await repository.Trailers.GetTrailerByRegistrationNumberAsync(truck.RegistrationNumber, false);
            if (trailerToCheck != null)
                return true;

            return false;
        }
    }
}
