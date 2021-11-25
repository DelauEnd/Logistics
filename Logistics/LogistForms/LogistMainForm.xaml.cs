using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Entities.Enums;
using Entities.Models;
using Entities.RequestFeautures;
using Entities.Utility;
using Logistics.Extensions;
using Logistics.Utility;

namespace Logistics.LogistForms
{
    /// <summary>
    /// Логика взаимодействия для LogistMainForm.xaml
    /// </summary>
    public partial class LogistMainForm : ExtendedWindow
    {
        public LogistMainForm(AuthenticatedUserInfo user)
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

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await SetDefaultOrders();
        }

        private async Task SetDefaultOrders()
        {
            var orders = await repository.Orders.GetAllOrdersAsync(new OrderParameters(), false);
            var mappedOrders = mapper.Map<IEnumerable<OrderDto>>(orders);
            UpdateSource(orderDt, mappedOrders);
        }

        private async void OrderDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedOrder = GetSelectedOrder();

            if (orderDt.Items.Count == 0 || SelectedOrder == null)
                return;

            await SetActualCargoes(SelectedOrder);
        }

        private async Task SetActualCargoes(OrderDto SelectedOrder)
        {
            var cargoes = await repository.Cargoes.GetCargoesByOrderIdAsync(SelectedOrder.Id, new CargoParameters(), false);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);
            UpdateSource(cargoDt, mappedCargoes);

            FillOrderFields(SelectedOrder);
        }

        private void CargoDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCargo = GetSelectedCargo();

            if (cargoDt.Items.Count == 0 || selectedCargo == null)
                return;

            FillCargoFields(selectedCargo);
        }

        private void FillCargoFields(CargoDto selectedCargo)
        {
            cargoHeight.Text = selectedCargo.Dimensions.Height.ToString();
            cargoLength.Text = selectedCargo.Dimensions.Length.ToString();
            cargoWidth.Text = selectedCargo.Dimensions.Width.ToString();
            cargoWeight.Text = selectedCargo.Weight.ToString();
        }

        private async void FillOrderFields(OrderDto SelectedOrder)
        {
            var destination = await repository.Customers.GetDestinationByOrderIdAsync(SelectedOrder.Id, false);
            FillDestinationInfo(destination);

            var sender = await repository.Customers.GetSenderByOrderIdAsync(SelectedOrder.Id, false);
            FillSenderInfo(sender);
        }

        private void FillSenderInfo(Customer sender)
        {
            addressSenderTb.Text = sender.Address;
            fioSenderTb.Text = sender.ContactPerson.Surname
                + " " + sender.ContactPerson.Name
                + " " + sender.ContactPerson.Patronymic;
            numberSenderTb.Text = sender.ContactPerson.PhoneNumber;
        }

        private void FillDestinationInfo(Customer destination)
        {
            addressDestinationTb.Text = destination.Address;
            fioDestinationTb.Text = destination.ContactPerson.Surname
                + " " + destination.ContactPerson.Name
                + " " + destination.ContactPerson.Patronymic;
            numberDestinationTb.Text = destination.ContactPerson.PhoneNumber;
        }

        private OrderDto GetSelectedOrder()
        {
            return orderDt.SelectedItem as OrderDto;
        }

        private CargoDto GetSelectedCargo()
        {
            return cargoDt.SelectedItem as CargoDto;
        }

        private void UpdateSource(DataGrid sender, IEnumerable<object> ItemSource)
        {
            sender.ItemsSource = ItemSource;

            if (sender.Items.Count != 0)
                sender.SelectedItem = sender.Items[0];

            if (GetSelectedOrder() == null)
                cargoDt.ItemsSource = null;
        }

        private async void SearchOrderBtnClick(object sender, RoutedEventArgs e)
        {
            var parameters = new OrderParameters { Search = searchOrderTb.Text };

            var orders = await repository.Orders.GetAllOrdersAsync(parameters, false);
            var mappedOrder = mapper.Map<IEnumerable<OrderDto>>(orders);

            UpdateSource(orderDt, mappedOrder);
        }

        private async void SearchOrderTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchOrderTb.Text))
            {
                var orders = await repository.Orders.GetAllOrdersAsync(new OrderParameters(), false);
                var mappedOrder = mapper.Map<IEnumerable<OrderDto>>(orders);

                UpdateSource(orderDt, mappedOrder);
            }

        }

        private void OnlyNumericInput(object sender, TextCompositionEventArgs e)
        {
            var box = sender as TextBox;

            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!box.Text.Contains(".")
               && box.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}
