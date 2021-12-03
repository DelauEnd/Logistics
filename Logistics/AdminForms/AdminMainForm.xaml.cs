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
            this.SetupWindowsStyle();
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
    }
}
