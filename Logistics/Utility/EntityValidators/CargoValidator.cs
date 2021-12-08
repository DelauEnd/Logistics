using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logistics.EntityValidators
{
    public class CargoValidator : IValidator<Cargo>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public CargoValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(Cargo entity)
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

        private void TryToValid(Cargo cargo)
        {
            if (cargo.ArrivalDate < cargo.DepartureDate)
                throw new Exception("Дата доставки не может быть раньше даты отправки");
        }
    }
}
