using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Utility;
using Logistics.Extensions;
using MaterialDesignThemes.Wpf;
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

namespace Logistics.AdminForms
{
    /// <summary>
    /// Логика взаимодействия для AdminMainForm.xaml
    /// </summary>
    public partial class AdminMainForm : ExtendedWindow
    {
        AuthenticatedUserInfo UserInfo { get; set; }

        public AdminMainForm(AuthenticatedUserInfo user)
        {
            InitializeComponent();
            this.SetupWindowStyle();
            ResizeMode = ResizeMode.CanResizeWithGrip;
            firstTab.IsChecked = true;
            UserInfo = user;
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
            await SetDefaults();
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

        private async Task SetupUserInfo()
        {
            var user = await repository.Users.GetUserByIdAsync(UserInfo.userId, false);
            UserFIO.Text = user.ContactPerson.Surname + " " + user.ContactPerson.Name + " " + user.ContactPerson.Patronymic;
        }

        private void TabChecked(object sender, RoutedEventArgs e)
        {
            var tab = sender as RadioButton;

            if (adminTab != null)
                adminTab.SelectedIndex = tab.TabIndex;
        }

        #region SetInfo
        private async Task SetDefaultUsers()
        {
            var users = await repository.Users.GetAllUsersAsync(false);
            var mappedUsers = mapper.Map<IEnumerable<UserWithLoginDto>>(users);
            UpdateSource(userDt, mappedUsers);
        }

        private async Task SetDefaultTrucks()
        {
            var trucks = await repository.Trucks.GetAllTrucksAsync(false);
            var mappedTrucks = mapper.Map<IEnumerable<TruckDto>>(trucks);
            UpdateSource(truckDt, mappedTrucks);
        }

        private async Task SetDefaultTrailers()
        {
            var trailers = await repository.Trailers.GetAllTrailersAsync(false);
            var mappedTrailers = mapper.Map<IEnumerable<TrailerDto>>(trailers);
            UpdateSource(trailerDt, mappedTrailers);
        }

        private async Task SetDefaultTypes()
        {
            var types = await repository.Types.GetAllTypes(false);
            var mappedTypes = mapper.Map<IEnumerable<CargoTypeDto>>(types);
            UpdateSource(cargoTypeDt, mappedTypes);
        }

        private async Task SetDefaults()
        {
            await SetDefaultTrailers();
            await SetDefaultTrucks();
            await SetDefaultTypes();
            await SetDefaultUsers();
        }

        #endregion

        private CargoTypeDto GetSelectedType()
            => cargoTypeDt.SelectedItem as CargoTypeDto;

        private UserWithLoginDto GetSelectedUser()
            => userDt.SelectedItem as UserWithLoginDto;

        private TruckDto GetSelectedTruck()
            => truckDt.SelectedItem as TruckDto;

        private TrailerDto GetSelectedTrailer()
            => trailerDt.SelectedItem as TrailerDto;

        #region userForm
        private async void AddUserClick(object sender, RoutedEventArgs e)
        {
            var userForm = new CreateUserForm();

            await HandleUserForm(userForm);
        }

        private async void EditUserClick(object sender, RoutedEventArgs e)
        {
            var selectedUser = GetSelectedUser();

            var userForm = new CreateUserForm(true);
            userForm.SetUserInfo(selectedUser);

            await HandleUserForm(userForm);
        }

        private async Task HandleUserForm(CreateUserForm userForm)
        {
            userForm.ShowDialog();

            if (!userForm.Created)
                return;

            await userForm.BuildUser();
            await SetDefaultUsers();
        }
        #endregion

        private async void AddTruckClick(object sender, RoutedEventArgs e)
        {
            var truckForm = new CreateTruckForm();

            await HandleTruckForm(truckForm);
        }

        private async void EditTruckClick(object sender, RoutedEventArgs e)
        {
            var selectedUser = GetSelectedTruck();

            var truckForm = new CreateTruckForm(true);
            await truckForm.SetTruckInfo(selectedUser);

            await HandleTruckForm(truckForm);
        }

        private async Task HandleTruckForm(CreateTruckForm truckForm)
        {
            truckForm.ShowDialog();

            if (!truckForm.Created)
                return;

            await truckForm.BuildTruck();
            await SetDefaultTrucks();
        }

        private async void AddTrailerClick(object sender, RoutedEventArgs e)
        {
            var trailerForm = new CreateTrailerForm();

            await HandleTrailerForm(trailerForm);
        }

        private async void EditTrailerClick(object sender, RoutedEventArgs e)
        {
            var selectedTrailer = GetSelectedTrailer();

            var trailerForm = new CreateTrailerForm(true);
            await trailerForm.SetTrailerInfo(selectedTrailer);

            await HandleTrailerForm(trailerForm);
        }

        private async Task HandleTrailerForm(CreateTrailerForm typeForm)
        {
            typeForm.ShowDialog();

            if (!typeForm.Created)
                return;

            await typeForm.BuildTrailer();
            await SetDefaultTrailers();
        }

        #region typeForm
        private async void AddTypeClick(object sender, RoutedEventArgs e)
        {
            var typeForm = new CreateCargoTypeForm();

            await HandleTypeForm(typeForm);
        }

        private async void EditTypeClick(object sender, RoutedEventArgs e)
        {
            var selectedType = GetSelectedType();

            var typeForm = new CreateCargoTypeForm(true);
            typeForm.SetCargoTypeInfo(selectedType);

            await HandleTypeForm(typeForm);
        }

        private async Task HandleTypeForm(CreateCargoTypeForm typeForm)
        {
            typeForm.ShowDialog();

            if (!typeForm.Created)
                return;

            await typeForm.BuildCargoType();
            await SetDefaultTypes();
        } 
        #endregion
    }
}
