using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Validators
{
    public interface ICargoValidator : IValidator<Cargo>
    {
        public bool TitleIsValid(string title);
        public bool DepartureDateIsValid(DateTime date);
        public bool ArrivalDateIsValid(DateTime departureDate, DateTime arrivalDate);
        public bool WeightOrDimensionIsValid(int value);
    }
}
