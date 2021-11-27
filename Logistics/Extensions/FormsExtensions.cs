using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Logistics.Extensions
{
    public static class FormsExtensions
    {
        public static void SetupWindowsStyle(this ExtendedWindow window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.None;
            window.ResizeMode = ResizeMode.NoResize;
            window.AllowsTransparency = true;
            window.Background = Application.Current.TryFindResource("SecondaryHueMidBrush") as Brush;
            window.MaxWidth = SystemParameters.WorkArea.Width;
            window.MaxHeight = SystemParameters.WorkArea.Height;
        }

        public static void MaximizeWindow(this ExtendedWindow window)
        {
            window.Width = window.MaxWidth;
            window.Height = window.MaxHeight;
            window.Left = 0;
            window.Top = 0;
            window.ResizeMode = ResizeMode.NoResize;
            window.draggable = false;
            window.maximazed = true;
        }

        public static void WindowSetToNormal(this ExtendedWindow window)
        {
            window.Width = 1000;
            window.Height = 600;
            window.Left = (SystemParameters.WorkArea.Width - window.Width) / 2;
            window.Top = (SystemParameters.WorkArea.Height - window.Height) / 2;
            window.ResizeMode = ResizeMode.CanResizeWithGrip;
            window.draggable = true;
            window.maximazed = false;
        }
    }
}
