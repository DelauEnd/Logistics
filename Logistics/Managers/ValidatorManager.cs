using Contracts;
using Contracts.Validators;
using Entities.Models;
using Logistics.EntityValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics
{
    public class ValidatorManager : IValidatorManager
    {
        private IValidator<Cargo> cargoValidator;
        private IValidator<CargoType> cargoTypeValidator;
        private IValidator<Customer> customerValidator;
        private IValidator<Order> orderValidator;
        private IValidator<Route> routeValidator;
        private IValidator<Trailer> trailerValidator;
        private IValidator<Truck> truckValidator;
        private IValidator<User> userValidator;

        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public ValidatorManager(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IValidator<CargoType> CargoTypeValidator
        {
            get
            {
                if (cargoTypeValidator == null)
                    cargoTypeValidator = new CargoTypeValidator(logger, repository);
                return cargoTypeValidator;
            }
        }

        public IValidator<Route> RouteValidator
        {
            get
            {
                if (routeValidator == null)
                    routeValidator = new RouteValidator(logger, repository);
                return routeValidator;
            }
        }

        public IValidator<Order> OrderValidator
        {
            get
            {
                if (orderValidator == null)
                    orderValidator = new OrderValidator(logger, repository);
                return orderValidator;
            }
        }

        public IValidator<Cargo> CargoValidator
        {
            get
            {
                if (cargoValidator == null)
                    cargoValidator = new CargoValidator(logger, repository);
                return cargoValidator;
            }
        }

        public IValidator<Customer> CustomerValidator
        {
            get
            {
                if (customerValidator == null)
                    customerValidator = new CustomerValidator(logger, repository);
                return customerValidator;
            }
        }

        public IValidator<User> UserValidator
        {
            get
            {
                if (userValidator == null)
                    userValidator = new UserValidator(logger, repository);
                return userValidator;
            }
        }

        public IValidator<Truck> TruckValidator
        {
            get
            {
                if (truckValidator == null)
                    truckValidator = new TruckValidator(logger, repository);
                return truckValidator;
            }
        }

        public IValidator<Trailer> TrailerValidator
        {
            get
            {
                if (trailerValidator == null)
                    trailerValidator = new TrailerValidator(logger, repository);
                return trailerValidator;
            }
        }
    }
}
