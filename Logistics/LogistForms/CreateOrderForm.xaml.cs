using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeautures;
using Entities.Utility;
using Logistics.Extensions;
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

namespace Logistics.LogistForms
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderForm.xaml
    /// </summary>
    public partial class CreateOrderForm : ExtendedWindow
    {
        private bool forUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? orderId { get; set; }
        private AuthenticatedUserInfo UserInfo { get; set; }

        public CreateOrderForm(AuthenticatedUserInfo userInfo, bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowStyle();
            firstTab.IsChecked = true;
            this.forUpdate = forUpdate;
            this.UserInfo = userInfo;
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

        private void TabChecked(object sender, RoutedEventArgs e)
        {
            var tab = sender as RadioButton;

            createTab.SelectedIndex = tab.TabIndex;
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (senderDt.ItemsSource == null || destinationDt.ItemsSource == null)
                await SetAvailebleCustomers();
            if (cargoesDt.ItemsSource == null)
                SetEmptyAddedCargoes();
        }

        private async Task SetAvailebleCustomers()
        {
            var customer = await repository.Customers.GetAllCustomersAsync(false);
            var mappedCustomer = mapper.Map<IEnumerable<CustomerDto>>(customer);

            senderDt.ItemsSource = mappedCustomer;
            destinationDt.ItemsSource = mappedCustomer;
        }

        public async Task SetAddedCargoesByOrderId(Guid id)
        {
            orderId = id;
            var cargoes = await repository.Cargoes.GetCargoesByOrderIdAsync(id, new CargoParameters(), false);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            UpdateSource(cargoesDt, mappedCargoes);
        }

        private void SetEmptyAddedCargoes()
        {
            var cargoes = new List<CargoDto>();

            UpdateSource(cargoesDt, cargoes);
        }

        public async Task SetCustomersByOrderId(Guid id)
        {
            await SetAvailebleCustomers();

            orderId = id;
            var orderDestination = await repository.Customers.GetDestinationByOrderIdAsync(id, false);
            var orderSender = await repository.Customers.GetSenderByOrderIdAsync(id, false);

            var destinations = senderDt.ItemsSource as List<CustomerDto>;
            var selectedDestination = destinations.Where(destination => destination.Id == destination.Id).FirstOrDefault();

            var senders = senderDt.ItemsSource as List<CustomerDto>;
            var selectedSender = senders.Where(sender => sender.Id == orderSender.Id).FirstOrDefault();
           
            destinationDt.SelectedItem = selectedDestination;
            senderDt.SelectedItem = selectedSender;
        }

        private async void AddCargoClick(object sender, RoutedEventArgs e)
        {
            var cargoForm = new CreateCargoForm();
            cargoForm.ShowDialog();

            if (!cargoForm.Created)
                return;

            var cargo = await cargoForm.BuildCargo();

            UpdateCargoDt(cargo);
        }

        private async void EditCargoClick(object sender, RoutedEventArgs e)
        {
            var selectedCargo = GetSelectedCargo();
            if (selectedCargo == null)
                return;

            var cargoForm = new CreateCargoForm();
            await cargoForm.SetCargoInfo(selectedCargo);
            cargoForm.ShowDialog();

            if (!cargoForm.Created)
                return;

            var cargo = await cargoForm.BuildCargo();

            UpdateCargoDt(cargo);
        }

        private void UpdateCargoDt(CargoDto mappedCargo)
        {
            var cargoes = cargoesDt.ItemsSource as List<CargoDto>;
            var cargoInDt = cargoes.Where(cargoInDt => cargoInDt.Id == mappedCargo.Id).FirstOrDefault();
            
            if (cargoInDt == null)
            {
                cargoes.Add(mappedCargo);
                cargoesDt.Items.Refresh();
            }
            else
            {
                cargoes.Remove(cargoInDt);
                cargoes.Add(mappedCargo);
                cargoesDt.Items.Refresh();
            }
        }

        private void RefreshCustomerDt()
        {
            senderDt.Items.Refresh();
            destinationDt.Items.Refresh();
        }

        private async void AddCustomerClick(object sender, RoutedEventArgs e)
        {
            var customerForm = new CreateCustomerForm();
            customerForm.ShowDialog();

            if (!customerForm.Created)
                return;

            await customerForm.BuildCargo();
            await SetAvailebleCustomers();
            RefreshCustomerDt();
        }

        private async void EditSenderClick(object sender, RoutedEventArgs e)
        {
            var selectedSender = GetSelectedSender();

            await EditCustomer(selectedSender);
        }

        private async void EditDestinationClick(object sender, RoutedEventArgs e)
        {
            var selectedDestination = GetSelectedDestination();

            await EditCustomer(selectedDestination);
        }

        private async Task EditCustomer(CustomerDto selectedCustomer)
        {
            var customerForm = new CreateCustomerForm(true);
            customerForm.SetCustomerInfo(selectedCustomer);
            customerForm.ShowDialog();

            if (!customerForm.Created)
                return;

            await customerForm.BuildCargo();
            await SetAvailebleCustomers();
            RefreshCustomerDt();
        }

        private CustomerDto GetSelectedDestination()
        {
            return destinationDt.SelectedItem as CustomerDto;
        }

        private CustomerDto GetSelectedSender()
        {
            return senderDt.SelectedItem as CustomerDto;
        }

        private CargoDto GetSelectedCargo()
        {
            return cargoesDt.SelectedItem as CargoDto;
        }

        private void SenderDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedSender = GetSelectedSender();
            if (selectedSender == null)
                return;

            SetSenderInfo(selectedSender);
        }

        private void SetSenderInfo(CustomerDto selectedSender)
        {
            addressSender.Text = selectedSender.Address;
            senderFIO.Text = selectedSender.ContactPerson.Surname + " "
                + selectedSender.ContactPerson.Name + " "
                + selectedSender.ContactPerson.Patronymic;
            senderPhoneNum.Text = selectedSender.ContactPerson.PhoneNumber;
        }

        private void DestinationDtSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDestination = GetSelectedDestination();
            if (selectedDestination == null)
                return;

            SetDestinationInfo(selectedDestination);
        }

        private void SetDestinationInfo(CustomerDto selectedDestination)
        {
            addressDestination.Text = selectedDestination.Address;
            destinationFIO.Text = selectedDestination.ContactPerson.Surname + " "
                + selectedDestination.ContactPerson.Name + " "
                + selectedDestination.ContactPerson.Patronymic;
            destinationPhoneNum.Text = selectedDestination.ContactPerson.PhoneNumber;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }

        public async Task BuildOrder()
        {
            var editedCargo = GetBuildedOrder();
            var cargoToEdit = mapper.Map<Order>(editedCargo);

            if (!forUpdate)
                repository.Orders.CreateOrder(cargoToEdit);
            else
                repository.Orders.UpdateOrder(cargoToEdit);
            await repository.SaveAsync();
        }

        private OrderForCreationDto GetBuildedOrder()
        {
            Guid id = (Guid)GetOrderId();

            var order = new OrderForCreationDto
            {
                Id = id,
                DestinationId = GetSelectedDestination().Id,
                SenderId = GetSelectedSender().Id,
                UserId = UserInfo.userId
            };

            return order;
        }

        private Guid? GetOrderId()
        {
            if (orderId == null)
                orderId = Guid.NewGuid();

            return orderId;
        }

        public async Task HandleCargoes()
        {
            foreach (var cargo in cargoesDt.ItemsSource as List<CargoDto>)
                await AddOrUpdate(cargo);
        }

        private async Task AddOrUpdate(CargoDto cargo)
        {
            var cargoToHandle = await repository.Cargoes.GetCargoByIdAsync(cargo.Id, false);
            var mappedCargo = await RemapCargo(cargo);
            mappedCargo.OrderId = (Guid)orderId;

            if (cargoToHandle == null)
                repository.Cargoes.CreateCargo(mappedCargo);
            else
            {
                mappedCargo.RouteId = cargoToHandle.RouteId;
                repository.Cargoes.UpdateCargo(mappedCargo);
            }
            await repository.SaveAsync();
        }

        private async Task<Cargo> RemapCargo(CargoDto cargo)
        {
            var type = await repository.Types.GetTypeByTitleAsync(cargo.Type, false);

            var remappedCargo = new Cargo
            {
                Dimensions = cargo.Dimensions,
                Id = cargo.Id,
                OrderId = (Guid)cargo.OrderId,
                Title = cargo.Title,
                TypeId = type.Id,
                Weight = cargo.Weight,
            };
            return remappedCargo;
        }
    }
}
