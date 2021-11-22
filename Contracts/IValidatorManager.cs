using Contracts.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IValidatorManager
    {
        ICargoValidator CargoValidator { get; }
        ICargoCategoryValidator CargoCategoryValidator { get; }
        ICustomerValidator CustomerValidator { get; }
    }
}
