using Entities.DataTransferObjects;
using Entities.Models;
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
    /// Логика взаимодействия для CreateCargoForm.xaml
    /// </summary>
    public partial class CreateCargoForm : ExtendedWindow
    {
        public bool Created { get; private set; } = false;
        public Guid? cargoId { get; set; }

        public CreateCargoForm()
        {
            InitializeComponent();
            this.SetupWindowStyle();
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

        public async Task SetCargoInfo(CargoDto cargo)
        {
            cargoId = cargo.Id;
            await SetupTypeSource();
            var types = cargoType.ItemsSource as IEnumerable<CargoTypeDto>;

            cargoHeight.Text = cargo.Dimensions.Height.ToString();
            cargoLength.Text = cargo.Dimensions.Length.ToString();
            cargoWidth.Text = cargo.Dimensions.Width.ToString();
            cargoWeight.Text = cargo.Weight.ToString();
            cargoTitle.Text = cargo.Title;

            var type = types.Where(type => type.Title == cargo.Type).FirstOrDefault();
            cargoType.SelectedItem = type;
        }

        public async Task<CargoDto> BuildCargo()
        {
            var editedCargo = GetBuildedCargo();
            var cargoToEdit = mapper.Map<Cargo>(editedCargo);
            var cargoToReturn = mapper.Map<CargoDto>(cargoToEdit);

            CargoType type = await repository.Types.GetTypeByIdAsync(cargoToEdit.TypeId, trackChanges: false);
            cargoToReturn.Type = type.Title;

            return cargoToReturn;
        }

        private CargoForCreationDto GetBuildedCargo()
        {
            Guid id = (Guid)GetCargoId();      

            var cargo = new CargoForCreationDto
            {
                Id = id,
                Title = cargoTitle.Text,
                Weight = double.Parse(cargoWeight.Text),
                OrderId = Guid.Empty,
                TypeId = (cargoType.SelectedItem as CargoTypeDto).Id,
                Dimensions = new Dimensions
                {
                    Height = double.Parse(cargoHeight.Text),
                    Length = double.Parse(cargoLength.Text),
                    Width = double.Parse(cargoWidth.Text)
                }
            };
            return cargo;
        }

        private Guid? GetCargoId()
        {
            if (cargoId == null)
                cargoId = Guid.NewGuid();

            return cargoId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (cargoType.ItemsSource == null)
                await SetupTypeSource();
        }

        private async Task SetupTypeSource()
        {
            var types = await repository.Types.GetAllTypes(false);
            var mappedTypes = mapper.Map <IEnumerable<CargoTypeDto>>(types);
            cargoType.ItemsSource = mappedTypes;
        } 
    }
}
