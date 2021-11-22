using Contracts;
using Contracts.Validators;
using Logistics.EntityValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics
{
    public class ValidatorManager : IValidatorManager
    {
        private readonly ILoggerManager logger;
        public ValidatorManager(ILoggerManager logger)
        {
            this.logger = logger;
        }

        private ICargoValidator cargoValidator;
        private ICargoCategoryValidator cargoCategoryValidator;
        private ICustomerValidator customerValidator;

        public ICargoCategoryValidator CargoCategoryValidator
        {
            get
            {
                if (cargoCategoryValidator == null)
                    cargoCategoryValidator = new CargoCategoryValidator(logger);
                return cargoCategoryValidator;
            }
        }

        public ICargoValidator CargoValidator
        {
            get
            {
                if (cargoValidator == null)
                    cargoValidator = new CargoValidator(logger);
                return cargoValidator;
            }
        }

        public ICustomerValidator CustomerValidator
        {
            get
            {
                if (customerValidator == null)
                    customerValidator = new CustomerValidator(logger);
                return customerValidator;
            }
        }
    }
}
