using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.EntityValidators
{
    public class CustomerValidator : ICustomerValidator
    {
        private readonly ILoggerManager logger;
        public CustomerValidator(ILoggerManager logger)
        {
            this.logger = logger;
        }

        public bool AddressIsValid(string address)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
