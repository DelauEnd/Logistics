using Contracts;
using Contracts.Validators;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logistics.EntityValidators
{
    public class OrderValidator : IValidator<Order>
    {
        private readonly ILoggerManager logger;
        private readonly IRepositoryManager repository;
        public OrderValidator(ILoggerManager logger, IRepositoryManager repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public bool IsValid(Order entity)
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

        private void TryToValid(Order order)
        {
            return;
        }
    }
}
