using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using Logistics.Utility.ExcelHandler.RouteSheetAdditions;
using Microsoft.Extensions.DependencyInjection;
using static Logistics.Utility.MapHandler.RouterResponce;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Logistics.Utility.MapHandler
{
    public class RouteHandler
    {
        private IRepositoryManager _repository;
        protected IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());
        public List<RouteUnit> Units { get; set; }

        private Guid RouteId { get; set; }

        public RouteHandler(Guid routeId)
        {
            RouteId = routeId;
            Units = new List<RouteUnit>();
        }

        public async Task Init()
        {
            await Units.Init(RouteId);
            Units = Units.SortByDate();
        }

        public string BuildRequestString()
        {
            string requestParams = string.Empty;

            foreach (var elem in Units)
                requestParams += elem.LatLng.Lng.ToString().Replace(",",".") + "," + elem.LatLng.Lat.ToString().Replace(",", ".") + ";";

            return requestParams.Remove(requestParams.Length - 1);
        }

        public List<GMapMarker> BuildMarkers()
        {
            var markers = new List<GMapMarker>();

            foreach (var unit in Units)
            {
                var marker = CreateMarker(unit);
                markers.Add(marker);
            }

            return markers;
        }

        private GMapMarker CreateMarker(RouteUnit unit)
        {
            var marker = new GMapMarker(new PointLatLng(unit.LatLng.Lat, unit.LatLng.Lng))
            {
                Offset = new Point(-12, -25),
                Shape = new Grid()
                {
                    Name = unit.Id.ToString()
                }
            };

            var img = new PackIcon
            {
                Kind = PackIconKind.MapMarker,
                Width = 25,
                Height = 25,
                Foreground = Application.Current.TryFindResource("PrimaryHueDarkBrush") as Brush
            };

            (marker.Shape as Grid).Children.Add(img);

            return marker;
        }

        public async Task<GMapRoute> BuildRoute(Root route)
        {
            var elem = await route.WaypointsToLatLng();
            var buildedRoute = new GMapRoute(elem);
            return buildedRoute;
        }
    }
}
