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
    public class TrailerValidator : IValidator<Trailer>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public TrailerValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(Trailer entity)
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

        private async void TryToValid(Trailer trailer)
        {
            if (await RegNumberAlredyExist(trailer))
                throw new Exception("Транспорт с таким номером уже существует");
        }

        private async Task<bool> RegNumberAlredyExist(Trailer trailer)
        {
            var truckToCheck = await repository.Trucks.GetTruckByRegistrationNumberAsync(trailer.RegistrationNumber, false);  
            if (truckToCheck != null)
                return true;

            var trailerToCheck = await repository.Trailers.GetTrailerByRegistrationNumberAsync(trailer.RegistrationNumber, false);
            if (trailerToCheck != null)
                return true;

            return false;
        }
    }
}
