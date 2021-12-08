using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IValidatorManager
    {
        IValidator<Cargo> CargoValidator { get; }
        IValidator<CargoType> CargoTypeValidator { get; }
        IValidator<Customer> CustomerValidator { get; }
        IValidator<Order> OrderValidator { get; }
        IValidator<Route> RouteValidator { get; }
        IValidator<Trailer> TrailerValidator { get; }
        IValidator<Truck> TruckValidator { get; }
        IValidator<User> UserValidator { get; }
    }
}
