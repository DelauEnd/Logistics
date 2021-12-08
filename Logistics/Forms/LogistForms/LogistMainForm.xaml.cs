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
using Logistics.Utility.ExcelHandler;
using MaterialDesignThemes.Wpf;

namespace Logistics.LogistForms
{
    /// <summary>
    /// Логика взаимодействия для LogistMainForm.xaml
    /// </summary>
    public partial class LogistMainForm : ExtendedWindow
    {
        AuthenticatedUserInfo UserInfo { get; set; }
        private bool routesLoaded = false;
        private bool ordersLoaded = false;

        public LogistMainForm(AuthenticatedUserInfo user)
        {
            InitializeComponent();
            this.SetupWindowStyle();
            ResizeMode = ResizeMode.CanResizeWithGrip;
            UserInfo = user;
            orderTabBtn.IsChecked = true;
        }

        private void WindowStateChanged(object sender, EventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (draggable)
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

        private void BtnMinimizeWindowsClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnResizeWindowsClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (this.maximazed)
            {
                this.WindowSetToNormal();
                ((PackIcon)btn.Content).Kind = PackIconKind.WindowMaximize;
            }
            else
            {
                this.MaximizeWindow();
                ((PackIcon)btn.Content).Kind = PackIconKind.WindowRestore;
            }
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await SetupUserInfo();
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

        private async void TabChange(object sender, RoutedEventArgs e)
        {
            var tab = sender as RadioButton;

            if (tabMenu != null)
                tabMenu.SelectedIndex = tab.TabIndex;

            await LoadeTabContentIfOpenedFirstTime(tab.TabIndex);
        }

        private async Task LoadeTabContentIfOpenedFirstTime(int tabIndex)
        {
            if (tabIndex == 0 && !ordersLoaded)
                await SetDefaultOrders();
            else if (tabIndex == 1 && !routesLoaded)
                await SetDefaultRoutes();
        }

        private async Task SetupUserInfo()
        {
            var user = await repository.Users.GetUserByIdAsync(UserInfo.userId, false);
            UserFIO.Text = user.ContactPerson.Surname + " " + user.ContactPerson.Name + " " + user.ContactPerson.Patronymic;
        }

        #region OrderTab
        private async Task SetDefaultOrders()
        {
            ordersLoaded = true;

            var orders = await repository.Orders.GetAllOrdersAsync(new OrderParameters(), false);
            var mappedOrders = mapper.Map<IEnumerable<OrderDto>>(orders);
            UpdateSource(orderDt, mappedOrders, cargoDt);
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

        private async void SearchCargoTbTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(routeDtSearch.Text))
            {
                var selectedOrder = GetSelectedOrder();
                var cargoes = await repository.Cargoes.GetCargoesByOrderIdAsync(selectedOrder.Id, new CargoParameters(), false);
                var mappedCargo = mapper.Map<IEnumerable<CargoDto>>(cargoes);

                UpdateSource(cargoDt, mappedCargo);
            }
        }

        private async void SearchCargoClick(object sender, RoutedEventArgs e)
        {
            var parameters = new CargoParameters { Search = searchCargoTb.Text };
           
            var selectedOrder = GetSelectedOrder();
            var cargoes = await repository.Cargoes.GetCargoesByOrderIdAsync(selectedOrder.Id, parameters, false);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            UpdateSource(cargoDt, mappedCargoes);
        }
        #endregion

        private RouteDto GetSelectedRoute()
        {
            return routeDt.SelectedItem as RouteDto;
        }

        private CargoDto GetSelectedCargoRoute()
        {
            return routeCargoDt.SelectedItem as CargoDto;
        }

        private async Task SetDefaultRoutes()
        {
            routesLoaded = true;

            var routes = await repository.Routes.GetAllRoutesAsync(new RouteParameters(), false);
            var mappedRoutes = mapper.Map<IEnumerable<RouteDto>>(routes);
            UpdateSource(routeDt, mappedRoutes);
        }

        private async void RouteDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var SelectedRoute = GetSelectedRoute();

            if (orderDt.Items.Count == 0 || SelectedRoute == null)
                return;

            await SetActualRouteCargoes(SelectedRoute);
        }

        private async Task SetActualRouteCargoes(RouteDto selectedRoute)
        {
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(selectedRoute.Id, new CargoParameters(), false);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);
            UpdateSource(routeCargoDt, mappedCargoes);

            await FillRouteFields(selectedRoute);
        }

        private async Task FillRouteFields(RouteDto selectedOrder)
        {
            var transport = await repository.Trucks.GetTruckByRegistrationNumberAsync(selectedOrder.TruckRegistrationNumber, true);
            driverFIO.Text = transport.Driver.Surname
                + " " + transport.Driver.Name
                + " " + transport.Driver.Patronymic;
            driverNumber.Text = transport.Driver.PhoneNumber;
        }

        private void RouteCargoDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void SearchRouteClick(object sender, RoutedEventArgs e)
        {
            var parameters = new RouteParameters { Search = routeDtSearch.Text };

            var routes = await repository.Routes.GetAllRoutesAsync(parameters, false);
            var mappedRoutes = mapper.Map<IEnumerable<RouteDto>>(routes);

            UpdateSource(routeDt, mappedRoutes);
        }

        private async void RouteDtSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(routeDtSearch.Text))
            {
                var routes = await repository.Routes.GetAllRoutesAsync(new RouteParameters(), false);
                var mappedRoute = mapper.Map<IEnumerable<RouteDto>>(routes);

                UpdateSource(routeDt, mappedRoute);
            }
        }

        private async void RouteCargoDtSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(routeCargoDtSearch.Text))
            {
                var selectedRoute = GetSelectedRoute();
                var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(selectedRoute.Id ,new CargoParameters(), false);
                var mappedCargo = mapper.Map<IEnumerable<CargoDto>>(cargoes);

                UpdateSource(routeCargoDt, mappedCargo);
            }
        }

        private async void RouteCargoSearchClick(object sender, RoutedEventArgs e)
        {
            var parameters = new CargoParameters { Search = routeCargoDtSearch.Text };
            var selectedRoute = GetSelectedRoute();

            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(selectedRoute.Id, parameters, false);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            UpdateSource(routeCargoDt, mappedCargoes);
        }

        private void PopupDateClosed(object sender, RoutedEventArgs e)
        {
            arrivalDatePicker.Text = "";
            departureDatePicker.Text = "";
        }

        private void PopupDateOpened(object sender, RoutedEventArgs e)
        {
            if (GetSelectedCargoRoute() == null)
                return;

            var selectedCargo = GetSelectedCargoRoute();
            arrivalDatePicker.SelectedDate = selectedCargo.ArrivalDate;
            departureDatePicker.SelectedDate = selectedCargo.DepartureDate;
        }

        private async void ApplyDateClick(object sender, RoutedEventArgs e)
        {
            var selectedCargo = GetSelectedCargoRoute();
            if (selectedCargo == null)
                return;

            var departureDate = departureDatePicker.SelectedDate;
            var arrivalDate = arrivalDatePicker.SelectedDate;

            if (MessageBox.Show("Дата будет изменена у всех грузов этого заказа на данном маршруте", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
                return;
            
            var orderToUpdate = await repository.Cargoes.GetCargoByIdAsync(selectedCargo.Id, true);

            orderToUpdate.DepartureDate = departureDate;
            orderToUpdate.ArrivalDate = arrivalDate;

            await repository.SaveAsync();
            await SetActualRouteCargoes(GetSelectedRoute());
        }

        private async void CreateRouteClick(object sender, RoutedEventArgs e)
        {
            var routeForm = new CreateRouteForm(UserInfo);
            routeForm.ShowDialog();

            if (!routeForm.Created)
                return;

            await routeForm.BuildRoute();
           
            await SetDefaultRoutes();
        }

        private async void EditRouteClick(object sender, RoutedEventArgs e)
        {
            var routeForm = new CreateRouteForm(UserInfo, true);
            await HandleRouteForm(routeForm);
            routeForm.ShowDialog();

            if (!routeForm.Created)
                return;

            await routeForm.BuildRoute();

            await SetDefaultRoutes();
        }

        private async Task<CreateRouteForm> HandleRouteForm(CreateRouteForm routeForm)
        {     
            var selectedRoute = GetSelectedRoute();
            await routeForm.SetAddedCargoesByRouteId(selectedRoute.Id);
            await routeForm.SetTrailerInfoByRegNumber(selectedRoute.TrailerRegistrationNumber);
            await routeForm.SetTruckInfoByRegNumber(selectedRoute.TruckRegistrationNumber);
            return routeForm;
        }

        private async void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            var orderForm = new CreateOrderForm(UserInfo);
            orderForm.ShowDialog();

            if (!orderForm.Created)
                return;

            await orderForm.BuildOrder();
            await orderForm.HandleCargoes();
            await SetDefaultOrders();
        }

        private async void UpdateOrderClick(object sender, RoutedEventArgs e)
        {
            var order = GetSelectedOrder();

            var orderForm = new CreateOrderForm(UserInfo, true);

            await orderForm.SetAddedCargoesByOrderId(order.Id);
            await orderForm.SetCustomersByOrderId(order.Id);
            orderForm.ShowDialog();

            if (!orderForm.Created)
                return;

            await orderForm.BuildOrder();
            await orderForm.HandleCargoes();
            await SetDefaultOrders();
        }

        private async void RemoveOrderClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = GetSelectedOrder();
            if (selectedOrder == null)
                return;

            var order = await repository.Orders.GetOrderByIdAsync(selectedOrder.Id, true);
            repository.Orders.DeleteOrder(order);
            await TryToSaveDeletions();
            await SetDefaultOrders();
        }

        private async void RemoveRouteClick(object sender, RoutedEventArgs e)
        {
            var selecterRoute = GetSelectedRoute();
            if (selecterRoute == null)
                return;

            var route = await repository.Routes.GetRouteByIdAsync(selecterRoute.Id, true);
            repository.Routes.DeleteRoute(route);
            await TryToSaveDeletions();
            await SetDefaultRoutes();
        }

        private async void CreateRouteSheetClick(object sender, RoutedEventArgs e)
        {
            var selecterRoute = GetSelectedRoute();
            if (selecterRoute == null)
                return;

            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(selecterRoute.Id, new CargoParameters(), false);
            if (cargoes.Any(cargo => cargo.DepartureDate == null || cargo.ArrivalDate == null))
            {
                MessageBox.Show("Невозможно выполнить для маршрута с недоставленными грузами");
                return;
            }
            
            var routeSheetHandler = new ExcelRouteSheet(selecterRoute.Id);
            await Task.Run(() => routeSheetHandler.BuildReport());
        }

        private async void ShowRouteClick(object sender, RoutedEventArgs e)
        {
            var selecterRoute = GetSelectedRoute();
            if (selecterRoute == null)
                return;

            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(selecterRoute.Id, new CargoParameters(), false);
            if (cargoes.Any(cargo => cargo.DepartureDate == null || cargo.ArrivalDate == null))
            {
                MessageBox.Show("Невозможно выполнить для маршрута с недоставленными грузами");
                return;
            }

            var routeSheetHandler = new RouteForm(selecterRoute.Id);
            routeSheetHandler.Show();
            await routeSheetHandler.BuildRoute();
        }
    }
}
