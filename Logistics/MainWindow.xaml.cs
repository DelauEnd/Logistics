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

namespace Logistics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ExtendedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
            var elem = await repository.Trucks.GetTruckByIdAsync(new Guid("9ac8905e-dd8e-43eb-b58d-9464da6fab6b"), true);
            MessageBox.Show(elem.Driver.Name);
            logger.LogInfo($"Logged successfull. !!!{elem.Driver.Name}!!! ");
        }
    }
}
