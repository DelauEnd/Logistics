using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Models.OwnedModels;
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

namespace Logistics.AdminForms
{
    /// <summary>
    /// Логика взаимодействия для CreateTruckForm.xaml
    /// </summary>
    public partial class CreateTruckForm : ExtendedWindow
    {
        private bool ForUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? TruckId { get; set; }
        private bool Truckable { get; set; }

        public CreateTruckForm(bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowStyle();
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

        public async Task SetTruckInfo(TruckDto truck)
        {
            TruckId = truck.Id;
            await SetupTypeSource();

            regNumber.Text = truck.RegistrationNumber;
            customerName.Text = truck.Driver.Name;
            customerPatronymic.Text = truck.Driver.Patronymic;
            customerSurname.Text = truck.Driver.Surname;
            customerPhoneNum.Text = truck.Driver.PhoneNumber;

            Truckable = CheckTruck(truck);
            
            if (Truckable)
                SetCargoedInfo(truck); 
        }

        private async void SetCargoedInfo(TruckDto truck)
        {
            height.Text = truck.LimitLoad.Height.ToString();
            length.Text = truck.LimitLoad.Length.ToString();
            width.Text = truck.LimitLoad.Width.ToString();
            weight.Text = truck.LoadCapacity.ToString();

            await SetupTypeSource();
            var types = cargoType.ItemsSource as IEnumerable<CargoTypeDto>;
            var type = types.Where(type => type.Title == truck.TransportedCargoType).FirstOrDefault();
            cargoType.SelectedItem = type;
        }

        private bool CheckTruck(TruckDto truck)
            => !string.IsNullOrWhiteSpace(truck.TransportedCargoType);

        public async Task BuildTruck()
        {
            var editedTruck = GetBuildedTruck();
            var truckToEdit = mapper.Map<Truck>(editedTruck);

            if (!ForUpdate)
                repository.Trucks.CreateTruck(truckToEdit);
            else
                repository.Trucks.UpdateTruck(truckToEdit);
            await repository.SaveAsync();
        }

        private TruckForCreationDto GetBuildedTruck()
        {
            TruckForCreationDto truck;

            if (Truckable)
                truck = BuildCargable();
            else
                truck = BuildNonCargable();
            return truck;
        }

        private TruckForCreationDto BuildNonCargable()
        {
            var id = (Guid)GetTruckId();

            var truck = new TruckForCreationDto
            {
                Id = id,
                Driver = new Person
                {
                    Name = customerName.Text,
                    Patronymic = customerPatronymic.Text,
                    Surname = customerSurname.Text,
                    PhoneNumber = customerPhoneNum.Text
                },
                RegistrationNumber = regNumber.Text
            };
            return truck;
        }

        private TruckForCreationDto BuildCargable()
        {
            var id = (Guid)GetTruckId();

            var truck = new TruckForCreationDto
            {
                Id = id,
                LoadCapacity = double.Parse(weight.Text),
                TransportedCargoTypeId = (cargoType.SelectedItem as CargoTypeDto).Id,
                Driver = new Person
                {
                    Name = customerName.Text,
                    Patronymic = customerPatronymic.Text,
                    Surname = customerSurname.Text,
                    PhoneNumber = customerPhoneNum.Text
                },
                LimitLoad = new Dimensions
                {
                    Height = double.Parse(height.Text),
                    Length = double.Parse(length.Text),
                    Width = double.Parse(width.Text)
                },
                RegistrationNumber = regNumber.Text
            };
            return truck;
        }

        private Guid? GetTruckId()
        {
            if (TruckId == null)
                TruckId = Guid.NewGuid();

            return TruckId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            await SetupTypeSource();
            canCarry.IsChecked = Truckable;    
        }

        private async Task SetupTypeSource()
        {
            if (cargoType.ItemsSource != null)
                return;
            var types = await repository.Types.GetAllTypes(false);
            var mappedTypes = mapper.Map<IEnumerable<CargoTypeDto>>(types);
            cargoType.ItemsSource = mappedTypes;
        }

        private void CanCarryChecked(object sender, RoutedEventArgs e)
        {
            var checker = sender as CheckBox;

            if (truckInfo == null)
                return;

            Truckable = (bool)checker.IsChecked;
            ChangeVisible(Truckable);
        }

        private void TruckInfoLoaded(object sender, RoutedEventArgs e)
        {
            ChangeVisible(Truckable);
        }

        private void ChangeVisible(bool shouldRender)
        {
            if (shouldRender)
                truckInfo.Visibility = Visibility.Visible;
            else
                truckInfo.Visibility = Visibility.Collapsed;
        }
    }
}
