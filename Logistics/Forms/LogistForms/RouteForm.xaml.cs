using GMap.NET;
using GMap.NET.MapProviders;
using Logistics.Extensions;
using Logistics.Utility.ExcelHandler.RouteSheetAdditions;
using Logistics.Utility.MapHandler;
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
    /// Логика взаимодействия для RouteForm.xaml
    /// </summary>
    public partial class RouteForm : ExtendedWindow
    {
        private Guid RouteId { get; set; }
        private List<RouteUnit> Units{ get; set; }

        public RouteForm(Guid routeId)
        {
            InitializeComponent();
            this.SetupWindowStyle();
            RouteId = routeId;
            ResizeMode = ResizeMode.CanResizeWithGrip;
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

        private void btnResizeWindowsClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            map.MapProvider = GMapProviders.OpenStreetMap;
            map.Zoom = 5;
            map.MaxZoom = 18;
            map.MinZoom = 1;

            map.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;

            map.CanDragMap = true;

            map.DragButton = MouseButton.Left;
        }

        public async Task BuildRoute()
        {
            await TryToBuildRoute();
        }

        private async Task TryToBuildRoute()
        {
            var router = new RouteHandler(RouteId);
            await router.Init();
            Units = router.Units;

            var requestStr = router.BuildRequestString();

            var web = new RequestHandler();
            var route = web.GetRouteByHttp(requestStr);
            var gmapRoute = await router.BuildRoute(route);
            map.Markers.Add(gmapRoute);

            var markers = router.BuildMarkers();
            foreach (var marker in markers)
            {
                (marker.Shape as Grid).MouseDown += MarkerClick;
                map.Markers.Add(marker);
            }
        }

        private void MarkerClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void OnMapDrag()
        {
            var upperBound = 84.7;
            var lowerBound = -84.7;
            var rightBound = 179.28;
            var leftBound = -179.28;

            if (map.Position.Lat > upperBound)
            {
                map.CenterPosition = new PointLatLng(upperBound, map.Position.Lng);
            }
            if (map.Position.Lat < lowerBound)
            {
                map.CenterPosition = new PointLatLng(lowerBound, map.Position.Lng);
            }
            if (map.Position.Lng > rightBound)
            {
                map.CenterPosition = new PointLatLng(map.Position.Lat, rightBound);
            }
            if (map.Position.Lng < leftBound)
            {
                map.CenterPosition = new PointLatLng(map.Position.Lat, leftBound);
            }
        }
    }
}
