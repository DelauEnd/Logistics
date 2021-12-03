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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contracts;
using Logistics.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Logistics.LogistForms;
using Entities.Enums;
using Logistics.AdminForms;

namespace Logistics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ExtendedWindow
    {
        private IAuthenticationManager _auth;
        private IAuthenticationManager auth => _auth ?? (_auth = App.ServiceProvider.GetService<IAuthenticationManager>());

        public MainWindow()
        {
            InitializeComponent();
            this.SetupWindowsStyle();
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

        private async void AuthorizationBtnClick(object sender, RoutedEventArgs e)
        {
            var user = await auth.Authenticate(loginBox.Text, passwordBox.Password);

            if (user == null)
                return;

            if (user.Role == UserRole.Logist)
                new LogistMainForm(user).Show();
            else if (user.Role == UserRole.Admin)
                new AdminMainForm(user).Show();

            Close();
        }
    }
}
