using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Models.OwnedModels;
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

namespace Logistics.AdminForms
{
    /// <summary>
    /// Логика взаимодействия для CreateCargoTypeForm.xaml
    /// </summary>
    public partial class CreateCargoTypeForm : ExtendedWindow
    {
        private bool ForUpdate { get; set; }
        public bool Created { get; private set; } = false;
        public Guid? TypeId { get; set; }

        public CreateCargoTypeForm(bool forUpdate = false)
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

        public void SetCargoTypeInfo(CargoTypeDto type)
        {
            TypeId = type.Id;

            cargoTypeTitle.Text = type.Title;
        }

        public async Task BuildCargoType()
        {
            var editedType = GetBuildedType();
            var mappedType = mapper.Map<CargoType>(editedType);

            if (!ForUpdate)
                repository.Types.CreateType(mappedType);
            else
                repository.Types.UpdateType(mappedType);
            await repository.SaveAsync();
        }

        private CargoTypeDto GetBuildedType()
        {
            Guid id = (Guid)GetTypeId();

            var type = new CargoTypeDto
            {
                Id = id,
                Title = cargoTypeTitle.Text
            };
            return type;
        }

        private Guid? GetTypeId()
        {
            if (TypeId == null)
                TypeId = Guid.NewGuid();

            return TypeId;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Created = true;
            Close();
        }
    }
}
