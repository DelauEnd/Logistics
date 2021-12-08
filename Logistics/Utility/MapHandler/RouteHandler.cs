using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using GMap.NET.WindowsPresentation;
using Logistics.Utility.ExcelHandler.RouteSheetAdditions;
using Microsoft.Extensions.DependencyInjection;
using static Logistics.Utility.MapHandler.RouterResponce;

namespace Logistics.Utility.MapHandler
{
    public class RouteHandler
    {
        private IRepositoryManager _repository;
        protected IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());
        List<RouteUnit> Units { get; set; }

        private Guid RouteId { get; set; }

        public RouteHandler(Guid routeId)
        {
            RouteId = routeId;
            Units = new List<RouteUnit>();
        }

        public async Task<string> BuildRequestString()
        {
            await Units.Init(RouteId);
            Units = Units.SortByDate();
            string requestParams = string.Empty;

            foreach (var elem in Units)
                requestParams += elem.LatLng.Lng.ToString().Replace(",",".") + "," + elem.LatLng.Lat.ToString().Replace(",", ".") + ";";

            return requestParams.Remove(requestParams.Length - 1);
        }

        public async Task<GMapRoute> BuildRoute(Root route)
        {
            var elem = await route.WaypointsToLatLng();
            var buildedRoute = new GMapRoute(elem);
            return buildedRoute;
        }
    }
}
