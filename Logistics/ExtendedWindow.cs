using Contracts;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Logistics.Utility;

namespace Logistics
{
    public class ExtendedWindow : Window
    {
        private IRepositoryManager _repository;
        protected IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());

        private ILoggerManager _logger;
        protected ILoggerManager logger => _logger ?? (_logger = App.ServiceProvider.GetService<ILoggerManager>());

        private IMapper _mapper;
        protected IMapper mapper => _mapper ?? (_mapper = App.ServiceProvider.GetService<IMapper>());

        private ITickTimer _timer;
        protected ITickTimer timer => _timer ?? (_timer = App.ServiceProvider.GetService<ITickTimer>());
    }
}
