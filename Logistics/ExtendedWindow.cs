﻿using Contracts;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Logistics.Utility;
using System.Windows.Controls;
using System.Collections.Generic;

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

        public bool draggable { get; set; } = true;
        public bool maximazed { get; set; }

        protected void UpdateSource(DataGrid sender, IEnumerable<object> ItemSource, DataGrid relatedTable = null)
        {
            sender.ItemsSource = ItemSource;

            if (sender.Items.Count != 0)
                sender.SelectedItem = sender.Items[0];

            if (sender.SelectedItem == null && relatedTable != null)
                relatedTable.ItemsSource = null;
        }
    }
}
