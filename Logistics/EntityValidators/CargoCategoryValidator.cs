using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.EntityValidators
{
    public class CargoCategoryValidator : ICargoCategoryValidator
    {
        private readonly ILoggerManager logger;
        public CargoCategoryValidator(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public bool IsValid(CargoCategory entity)
        {
            throw new NotImplementedException();
        }

        public bool TitleIsValid(string title)
        {
            throw new NotImplementedException();
        }
    }
}
