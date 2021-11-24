using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeautures;
using Logistics.Extensions;

namespace Logistics.LogistForms
{
    /// <summary>
    /// Логика взаимодействия для LogistMainForm.xaml
    /// </summary>
    public partial class LogistMainForm : ExtendedWindow
    {
        public LogistMainForm()
        {
            InitializeComponent();
            this.SetupWindowsStyle();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragWindow(e);
        }

        private void DragWindow(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnResizeWindowsClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void WindowLoadedAsync(object sender, RoutedEventArgs e)
        {
            await PreInitData();
        }

        private async Task PreInitData()
        {
            var elem = await repository.Orders.GetAllOrdersAsync(new OrderParameters(), false);
            await UpdateOrderData(elem);
        }

        private async Task UpdateOrderData(IEnumerable<Order> source)
        {
            var orderDto = mapper.Map<IEnumerable<OrderDto>>(source);
            orderDt.ItemsSource = orderDto;
            if(orderDto.Any())
                await SetupOrderInformation(orderDto.First());
        }

        private async Task SetupOrderInformation(OrderDto elem)
        {
            await SetupSenderAndDestination(elem.Id);
            await SetupSenderAndDestination(elem.Id);
            await SetupSenderAndDestination(elem.Id);
            var cargoes = await GetCargoesDtoByOrderId(elem.Id);
            UpdateCargoData(cargoes);
        }

        private async Task SetupSenderAndDestination(Guid Id)
        {
            await SetupDestinationData(Id);
            await SetupSenderData(Id);
        }

        private async Task SetupDestinationData(Guid cargoId)
        {
            var destination = await repository.Customers.GetDestinationByOrderIdAsync(cargoId, false);
            FillDestinationFields(destination);
        }

        private async Task SetupSenderData(Guid cargoId)
        {
            var sender = await repository.Customers.GetSenderByOrderIdAsync(cargoId, false);
            FillSenderFields(sender);
        }

        private void FillDestinationFields(Customer destination)
        {
            addressDestinationTb.Text = destination.Address;
            fioDestinationTb.Text = destination.ContactPerson.Name + " " + destination.ContactPerson.Surname + " " + destination.ContactPerson.Patronymic;
            numberDestinationTb.Text = destination.ContactPerson.PhoneNumber;
        }

        private void FillSenderFields(Customer sender)
        {
            addressSenderTb.Text = sender.Address;
            fioSenderTb.Text = sender.ContactPerson.Name + " " + sender.ContactPerson.Surname + " " + sender.ContactPerson.Patronymic;
            numberSenderTb.Text = sender.ContactPerson.PhoneNumber;
        }

        private async Task<IEnumerable<CargoDto>> GetCargoesDtoByOrderId(Guid id)
        {
            var cargoes = await repository.Cargoes.GetCargoesByOrderIdAsync(id, new CargoParameters(), false);
            var cargoesDto = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            return cargoesDto;
        }

        private void UpdateCargoData(IEnumerable<CargoDto> cargoes)
        {
            cargoDt.ItemsSource = cargoes;
            FillCargoFields(cargoes.First());
        }

        private void FillCargoFields(CargoDto cargo)
        {
            cargoHeight.Text = cargo.Dimensions.Height.ToString();
            cargoLength.Text = cargo.Dimensions.Length.ToString();
            cargoWidth.Text = cargo.Dimensions.Width.ToString();
            cargoWeight.Text = cargo.Weight.ToString();
        }

        private async void OrderDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderDt.SelectedItem != null)
                await SetOrdersForSelected();
        }

        private async Task SetOrdersForSelected()
        {
            var newSelectedOrder = orderDt.SelectedItem as OrderDto;
            await SetupOrderInformation(newSelectedOrder);
        }

        private void CargoDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cargoDt.SelectedItem != null)
                SetActualCargoes();
        }

        private void SetActualCargoes()
        {
            var newSelectedCargo = (cargoDt.SelectedItem ?? cargoDt.Items[0]) as CargoDto;
            FillCargoFields(newSelectedCargo);
        }

        private async void SearchOrderTbTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchOrderTb.Text))
                AddSearchOrderTickEventIfNotExist();
            else
            {
                DeleteSearchOrderTickEventIfExist();
                await PreInitData();
            }
        }

        private void DeleteSearchOrderTickEventIfExist()
        {
            if (timer.EventsList.Contains(nameof(SearchForTimerTick)))
                DeleteSearchOrderTickEvent();
        }

        private void AddSearchOrderTickEventIfNotExist()
        {
            if (!timer.EventsList.Contains(nameof(SearchForTimerTick)))
                AddSearchOrderTickEvent();
        }

        private void DeleteSearchOrderTickEvent()
        {
            timer.EventsList.Remove(nameof(SearchForTimerTick));
            timer.Timer.Tick -= SearchForTimerTick;
        }

        private void AddSearchOrderTickEvent()
        {
            timer.EventsList.Add(nameof(SearchForTimerTick));
            timer.Timer.Tick += SearchForTimerTick;
        }

        private async void SearchForTimerTick(object sender, EventArgs e)
        {
            DeleteSearchOrderTickEvent();
            var parameters = new OrderParameters
            {
                Search = searchOrderTb.Text
            };
            var order = await repository.Orders.GetAllOrdersAsync(parameters, false);

            if (order.Count() == 0)
                NullableTableSources();
            else
                await UpdateOrderData(order);
        }

        private void NullableTableSources()
        {
            orderDt.ItemsSource = null;
            cargoDt.ItemsSource = null;
        }
    }
}
