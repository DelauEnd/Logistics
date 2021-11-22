using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Validators
{
    public interface ICustomerValidator : IValidator<Customer>
    {
        public bool AddressIsValid(string address);
    }
}
