using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logistics.EntityValidators
{
    public class RouteValidator : IValidator<Route>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public RouteValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(Route entity)
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

        private void TryToValid(Route customer)
        {
            return;
        }
    }
}
