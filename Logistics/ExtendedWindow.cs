using Contracts;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics
{
    public class ExtendedWindow : Window
    {
        private IRepositoryManager _repository;
        protected IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());

        private ILoggerManager _logger;
        protected ILoggerManager logger => _logger ?? (_logger = App.ServiceProvider.GetService<ILoggerManager>());
    }
}
