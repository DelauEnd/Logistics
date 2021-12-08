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
    /// Логика взаимодействия для CreateTrailerForm.xaml
    /// </summary>
    public partial class CreateTrailerForm : ExtendedWindow
    {
        private bool ForUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? TrailerId { get; set; }

        public CreateTrailerForm(bool forUpdate = false)
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

        public async Task SetTrailerInfo(TrailerDto trailer)
        {
            TrailerId = trailer.Id;
            await SetupTypeSource();
            var types = cargoType.ItemsSource as IEnumerable<CargoTypeDto>;

            height.Text = trailer.LimitLoad.Height.ToString();
            length.Text = trailer.LimitLoad.Length.ToString();
            width.Text = trailer.LimitLoad.Width.ToString();
            weight.Text = trailer.LoadCapacity.ToString();
            regNumber.Text = trailer.RegistrationNumber;

            var type = types.Where(type => type.Title == trailer.TransportedCargoType).FirstOrDefault();
            cargoType.SelectedItem = type;
        }

        public async Task BuildTrailer()
        {
            var editedTrailer = GetBuildedTrailer();
            if (editedTrailer == null)
                return;
            var trailerToEdit = mapper.Map<Trailer>(editedTrailer);

            if (!ForUpdate)
                repository.Trailers.CreateTrailer(trailerToEdit);
            else
                repository.Trailers.UpdateTrailer(trailerToEdit);
            await repository.SaveAsync();
        }

        private TrailerForCreationDto GetBuildedTrailer()
        {
            try
            {
                return TryToBuildRoute();
            }
            catch (Exception e)
            {
                HandleSaveExcetion(e);
                return null;
            }
        }

        private TrailerForCreationDto TryToBuildRoute()
        {
            Guid id = (Guid)GetCargoId();

            var trailer = new TrailerForCreationDto
            {
                Id = id,
                LoadCapacity = double.Parse(weight.Text),
                TransportedCargoTypeId = (cargoType.SelectedItem as CargoTypeDto).Id,
                LimitLoad = new Dimensions
                {
                    Height = double.Parse(height.Text),
                    Length = double.Parse(length.Text),
                    Width = double.Parse(width.Text)
                },
                RegistrationNumber = regNumber.Text
            };
            return trailer;
        }

        private Guid? GetCargoId()
        {
            if (TrailerId == null)
                TrailerId = Guid.NewGuid();

            return TrailerId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
                await SetupTypeSource();
        }

        private async Task SetupTypeSource()
        {
            if (cargoType.ItemsSource != null)
                return;
            var types = await repository.Types.GetAllTypes(false);
            var mappedTypes = mapper.Map<IEnumerable<CargoTypeDto>>(types);
            cargoType.ItemsSource = mappedTypes;
        }
    }
}
