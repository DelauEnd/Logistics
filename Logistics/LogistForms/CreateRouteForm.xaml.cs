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
    /// Логика взаимодействия для CreateRouteForm.xaml
    /// </summary>
    public partial class CreateRouteForm : ExtendedWindow
    {
        private bool forUpdate { get; set; }
        public bool routeCreated { get; private set; } = false;
        public Guid? routeId { get; set; }
        private AuthenticatedUserInfo UserInfo { get; set; }

        public CreateRouteForm(AuthenticatedUserInfo userInfo, bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowsStyle();
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
            await SetAvailebleCargoes();
            if (addedCargoes.ItemsSource == null)
                SetEmptyAddedCargoes();
            if (truckDt.ItemsSource == null)
                await SetAvailebleTrucks();
            if (trailerDt.ItemsSource == null)
                await SetAvailebleTrailers();
        }

        private async Task SetAvailebleTrucks()
        {
            var trucks = await repository.Trucks.GetAllTrucksAsync(false);
            var mappedTrucks = mapper.Map<IEnumerable<TruckDto>>(trucks);

            truckDt.ItemsSource = mappedTrucks;
        }

        private async Task SetAvailebleTrailers()
        {
            var trailers = await repository.Trailers.GetAllTrailersAsync(false);
            var mappedTrailers = mapper.Map<IEnumerable<TrailerDto>>(trailers);

            trailerDt.ItemsSource = mappedTrailers;
        }

        public async Task SetAddedCargoesByRouteId(Guid id)
        {
            routeId = id;
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(id, new CargoParameters(), true);
            var mappedCargoes = mapper.Map<IEnumerable<Cargo>>(cargoes);

            UpdateSource(addedCargoes, mappedCargoes);
            addedCargoes.Items.Refresh();
        }

        private void SetEmptyAddedCargoes()
        {
            var cargoes = new List<Cargo>();

            UpdateSource(addedCargoes, cargoes);
        }

        private async Task SetAvailebleCargoes()
        {
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(null, new CargoParameters(), true);

            UpdateSource(availebleCargoes, cargoes);
        }

        private Cargo GetAvailebleSelectedCargo()
        {
            return availebleCargoes.SelectedItem as Cargo;
        }

        private Cargo GetAddedSelectedCargo()
        {
            return addedCargoes.SelectedItem as Cargo;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var selectedCargo = GetAvailebleSelectedCargo();
            if (selectedCargo == null)
                return;

            var addedSource = addedCargoes.ItemsSource as List<Cargo>;
            var availebleSource = availebleCargoes.ItemsSource as List<Cargo>;

            addedSource.Add(selectedCargo);
            availebleSource.Remove(selectedCargo);

            UpdateSource(addedCargoes, addedSource);
            UpdateSource(availebleCargoes, availebleSource);
            RefreshTables();
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            var selectedCargo = GetAddedSelectedCargo();
            if (selectedCargo == null)
                return;

            var addedSource = addedCargoes.ItemsSource as List<Cargo>;
            var availebleSource = availebleCargoes.ItemsSource as List<Cargo>;

            addedSource.Remove(selectedCargo);
            availebleSource.Add(selectedCargo);

            UpdateSource(addedCargoes, addedSource);
            UpdateSource(availebleCargoes, availebleSource);
            RefreshTables();
        }

        private void RefreshTables()
        {
            addedCargoes.Items.Refresh();
            availebleCargoes.Items.Refresh();
        }

        private void CancelTruckClick(object sender, RoutedEventArgs e)
        {
            truckDt.SelectedItem = null;
            ClearTruckInfo();
        }

        private void CancelTrailerClick(object sender, RoutedEventArgs e)
        {
            trailerDt.SelectedItem = null;
            ClearTrailerInfo();
        }

        private TrailerDto GetSelectedTrailer()
        {
            return trailerDt.SelectedItem as TrailerDto;
        }

        private TruckDto GetSelectedTruck()
        {
            return truckDt.SelectedItem as TruckDto;
        }

        private void TrailerDtSelected(object sender, RoutedEventArgs e)
        {
            var trailer = GetSelectedTrailer();
            if (trailer == null)
                return;

            SetTrailerInfo(trailer);
        }

        private void TrucksDtSelected(object sender, RoutedEventArgs e)
        {
            var truck = GetSelectedTruck();
            if (truck == null)
                return;

            SetTruckInfo(truck);
        }

        public async Task SetTruckInfoByRegNumber(string regNum)
        {
            if (regNum == null)
                return;

            await SetAvailebleTrucks();

            var trucks = truckDt.ItemsSource as List<TruckDto>;
            var selectedTruck = trucks.Where(truck=>truck.RegistrationNumber == regNum).FirstOrDefault();

            truckDt.SelectedItem = selectedTruck;
        }

        public async Task SetTrailerInfoByRegNumber(string regNum)
        {
            if (regNum == null)
                return;

            await SetAvailebleTrailers();

            var trailer = trailerDt.ItemsSource as List<TrailerDto>;
            var selectedTruck = trailer.Where(trailer => trailer.RegistrationNumber == regNum).FirstOrDefault();

            trailerDt.SelectedItem = selectedTruck;
        }

        private void SetTruckInfo(TruckDto truck)
        {
            selectedTruck.Text = GetSelectedTruck().RegistrationNumber;
            cargoHeightTruck.Text = truck.LimitLoad.Height.ToString() ?? "-";
            cargoLengthTruck.Text = truck.LimitLoad.Length.ToString() ?? "-";
            cargoWidthTruck.Text = truck.LimitLoad.Width.ToString() ?? "-";
            cargoWeightTruck.Text = truck.LoadCapacity.ToString();
            carrierFIO.Text = truck.Driver.Name + " "
                + truck.Driver.Surname + " "
                + truck.Driver.Patronymic;
            phoneNum.Text = truck.Driver.PhoneNumber;
        }

        private void ClearTruckInfo()
        {
            selectedTruck.Text = "-";
            cargoHeightTruck.Text = "-";
            cargoLengthTruck.Text = "-";
            cargoWidthTruck.Text = "-";
            cargoWeightTruck.Text = "-";
            carrierFIO.Text = "-";
            phoneNum.Text = "-";
        }

        private void SetTrailerInfo(TrailerDto trailer)
        {
            selectedTrailer.Text = GetSelectedTrailer().RegistrationNumber;
            cargoHeightTrailer.Text = trailer.LimitLoad.Height.ToString();
            cargoLengthTrailer.Text = trailer.LimitLoad.Length.ToString();
            cargoWidthTrailer.Text = trailer.LimitLoad.Width.ToString();
            cargoWeightTrailer.Text = trailer.LoadCapacity.ToString();
        }

        private void ClearTrailerInfo()
        {
            selectedTrailer.Text = "-";
            cargoHeightTrailer.Text = "-";
            cargoLengthTrailer.Text = "-";
            cargoWidthTrailer.Text = "-";
            cargoWeightTrailer.Text = "-";
        }

        private async Task<RouteForCreationDto> GetBuildRoute()
        {
            var truck = await repository.Trucks.GetTruckByRegistrationNumberAsync(selectedTruck.Text, false);     
            Guid? trailerId = await GetTrailerId();
            Guid id = (Guid)GetRouteId();

            var route = new RouteForCreationDto
            {
                UserId = UserInfo.userId,
                Id = id ,
                TrailerId = trailerId,
                TruckId = truck.Id
            };
            return route;
        }

        public async Task BuildRoute()
        {
            var editedRoute = await GetBuildRoute();
            var routeToEdit = mapper.Map<Route>(editedRoute);

            if (forUpdate)
                repository.Routes.UpdateRoute(routeToEdit);
            else
                repository.Routes.CreateRoute(routeToEdit);

            await repository.SaveAsync();
            await MarkCargoesToRoute();
            await UnmarkCargoesFromRoute();
        }

        public async Task MarkCargoesToRoute()
        {
            var cargoes = addedCargoes.ItemsSource as IEnumerable<Cargo>;
            foreach (var cargo in cargoes)
            {
                await SetCargoToRoute(cargo, routeId); ;
            }
        }

        public async Task UnmarkCargoesFromRoute()
        {
            var cargoes = availebleCargoes.ItemsSource as IEnumerable<Cargo>;
            foreach (var cargo in cargoes)
            {
                await SetCargoToRoute(cargo, null);
            }
        }

        private async Task SetCargoToRoute(Cargo cargo, Guid? routeId)
        {
            var cargoToUpdate = await repository.Cargoes.GetCargoByIdAsync(cargo.Id, true);
            cargoToUpdate.RouteId = routeId;
            await repository.SaveAsync();
        }

        private Guid? GetRouteId()
        {
            if (routeId == null)
                routeId = Guid.NewGuid();

            return routeId;               
        }

        private async Task<Guid?> GetTrailerId()
        {
            var trailer = await repository.Trailers.GetTrailerByRegistrationNumberAsync(selectedTrailer.Text, false);
            Guid? trailerId = trailer != null ? trailer.Id as Guid? : null;
            return trailerId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            routeCreated = true;
            Close();
        }
    }
}
