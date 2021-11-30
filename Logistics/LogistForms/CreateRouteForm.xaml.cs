using Entities.DataTransferObjects;
using Entities.RequestFeautures;
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
    /// Логика взаимодействия для CreateRouteForm.xaml
    /// </summary>
    public partial class CreateRouteForm : ExtendedWindow
    {
        public CreateRouteForm()
        {
            InitializeComponent();
            this.SetupWindowsStyle();
            firstTab.IsChecked = true;
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
        }

        public async Task SetAddedCargoesByRouteId(Guid id)
        {
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(id, new CargoParameters(), true);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            UpdateSource(addedCargoes, mappedCargoes);
            addedCargoes.Items.Refresh();
        }

        private void SetEmptyAddedCargoes()
        {
            var cargoes = new List<CargoDto>();

            UpdateSource(addedCargoes, cargoes);
        }

        private async Task SetAvailebleCargoes()
        {
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(null, new CargoParameters(), true);
            var mappedCargoes = mapper.Map<IEnumerable<CargoDto>>(cargoes);

            UpdateSource(availebleCargoes, mappedCargoes);
        }

        private CargoDto GetAvailebleSelectedCargo()
        {
            return availebleCargoes.SelectedItem as CargoDto;
        }

        private CargoDto GetAddedSelectedCargo()
        {
            return addedCargoes.SelectedItem as CargoDto;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var selectedCargo = GetAvailebleSelectedCargo();
            if (selectedCargo == null)
                return;

            var addedSource = addedCargoes.ItemsSource as List<CargoDto>;
            var availebleSource = availebleCargoes.ItemsSource as List<CargoDto>;

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

            var addedSource = addedCargoes.ItemsSource as List<CargoDto>;
            var availebleSource = availebleCargoes.ItemsSource as List<CargoDto>;

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
    }
}
