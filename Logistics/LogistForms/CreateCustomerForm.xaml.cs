using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Models.OwnedModels;
using Entities.Utility;
using Logistics.Extensions;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CreateCustomerForm.xaml
    /// </summary>
    public partial class CreateCustomerForm : ExtendedWindow
    {
        private bool ForUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? CustomerId { get; set; }

        public CreateCustomerForm(bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowsStyle();
            this.ForUpdate = forUpdate;
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

        public void SetCustomerInfo(CustomerDto customer)
        {
            CustomerId = customer.Id;

            customerName.Text = customer.ContactPerson.Name;
            customerPatronymic.Text = customer.ContactPerson.Patronymic;
            customerSurname.Text = customer.ContactPerson.Surname;
            customerPhoneNum.Text = customer.ContactPerson.PhoneNumber;
            addressCustomer.Text = customer.Address;
            latText.Text = customer.Coordinates.Latitude.ToString();
            lngText.Text = customer.Coordinates.Latitude.ToString();
        }

        public async Task BuildCargo()
        {
            var editedCustomer = GetBuildedCargo();
            var customerToSave = mapper.Map<Customer>(editedCustomer);

            if (!ForUpdate)
                repository.Customers.CreateCustomer(customerToSave);
            else
                repository.Customers.UpdateCustomer(customerToSave);
            await repository.SaveAsync();
        }

        private CustomerForCreationDto GetBuildedCargo()
        {
            Guid id = (Guid)GetCustomerId();

            var customer = new CustomerForCreationDto
            {
                Id = id,
                Address = addressCustomer.Text,
                Coordinates = new LatLng
                {
                    Latitude = int.Parse(latText.Text),
                    Longitude = int.Parse(lngText.Text)
                },
                ContactPerson = new Person
                {
                     Name = customerName.Text,
                     Patronymic = customerPatronymic.Text,
                     Surname = customerSurname.Text,
                     PhoneNumber = customerPhoneNum.Text
                }
            };
            return customer;
        }

        private Guid? GetCustomerId()
        {
            if (CustomerId == null)
                CustomerId = Guid.NewGuid();

            return CustomerId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }
    }
}
