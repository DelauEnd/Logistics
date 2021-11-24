using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Logistics.Extensions
{
    public static class FormsExtensions
    {
        public static void SetupWindowsStyle(this Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.None;
            window.ResizeMode = ResizeMode.NoResize;
            window.AllowsTransparency = true;
            window.Background = Application.Current.TryFindResource("SecondaryHueMidBrush") as Brush;
        }
    }
}
