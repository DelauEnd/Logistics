using Entities.Utility;
using Logistics.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
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
        private bool forUpdate { get; set; }
        public bool routeCreated { get; private set; } = false;
        public Guid? routeId { get; set; }
        private AuthenticatedUserInfo UserInfo { get; set; }

        public CreateCustomerForm(bool forUpdate = false)
        {
            InitializeComponent();
            this.SetupWindowsStyle();
            this.forUpdate = forUpdate;
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

        private async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            //await SetAvailebleCargoes();
            //if (addedCargoes.ItemsSource == null)
            //    SetEmptyAddedCargoes();
            //if (truckDt.ItemsSource == null)
            //    await SetAvailebleTrucks();
            //if (trailerDt.ItemsSource == null)
            //    await SetAvailebleTrailers();
        }
    }
}
