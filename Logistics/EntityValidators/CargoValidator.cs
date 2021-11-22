using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.EntityValidators
{
    public class CargoValidator : ICargoValidator
    {
        private readonly ILoggerManager logger;
        public CargoValidator(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public bool IsValid(Cargo entity)
        {
            throw new NotImplementedException();
        }

        public bool ArrivalDateIsValid(DateTime departureDate, DateTime arrivalDate )
        {
            throw new NotImplementedException();
        }

        public bool DepartureDateIsValid(DateTime date)
        {
            throw new NotImplementedException();
        }

        public bool TitleIsValid(string title)
        {
            throw new NotImplementedException();
        }

        public bool WeightOrDimensionIsValid(int value)
        {
            throw new NotImplementedException();
        }
    }
}
