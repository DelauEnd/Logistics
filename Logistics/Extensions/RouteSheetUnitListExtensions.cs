using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.RequestFeautures;
using Entities.Models;
using GMap.NET;

namespace Logistics.Utility.ExcelHandler.RouteSheetAdditions
{
    public static class RouteSheetUnitListExtensions
    {
        private static IRepositoryManager _repository;
        private static IRepositoryManager repository => _repository ?? (_repository = App.ServiceProvider.GetService<IRepositoryManager>());

        static RouteUnit DestinationBuffer { get; set; }
        static RouteUnit SenderBuffer { get; set; }


        public static List<RouteUnit> SortByDate(this List<RouteUnit> list)
            => list.OrderBy(unit => unit.Date).ToList();

        public static async Task<List<RouteUnit>> Init(this List<RouteUnit> list, Guid RouteId)
        {
            var cargoes = await repository.Cargoes.GetCargoesByRouteIdAsync(RouteId, new CargoParameters(), false);

            foreach (var cargo in cargoes)
                await TryToAddInfo(cargo, list, RouteId);

            return list;
        }

        private static async Task TryToAddInfo(Cargo cargo, List<RouteUnit> list, Guid routeId)
        {
            SenderBuffer = new RouteUnit();
            DestinationBuffer = new RouteUnit();

            await BuildSenderBuffer(cargo, routeId);
            await BuildDestinationBuffer(cargo, routeId);

            AddBuffersIfNotAdded(cargo, list);

            DestinationBuffer = null;
            SenderBuffer = null;
        }

        private static void AddBuffersIfNotAdded(Cargo cargo, List<RouteUnit> list)
        {
            if (list.Where(x => x.Number == cargo.OrderId).Count() != 2)
            {
                DestinationBuffer.Id = list.Count() + 1;
                list.Add(DestinationBuffer);

                SenderBuffer.Id = list.Count() + 1;
                list.Add(SenderBuffer);
            }
        }

        private static async Task BuildDestinationBuffer(Cargo cargo, Guid routeId)
        {
            var destination = await repository.Customers.GetDestinationByOrderIdAsync(cargo.OrderId, false);
            var address = destination.Address;
            var truck = await repository.Trucks.GetTruckByRouteIdAsync(routeId, false);
            var driver = truck.Driver.Surname + " "
                + truck.Driver.Name + " "
                + truck.Driver.Patronymic;

            
            DestinationBuffer.LatLng = new PointLatLng(destination.Coordinates.Latitude, destination.Coordinates.Longitude);
            DestinationBuffer.Date = (DateTime)cargo.ArrivalDate;
            DestinationBuffer.Purpose = Purpose.Unloading;
            DestinationBuffer.Number = cargo.OrderId;
            DestinationBuffer.Address = address;
            DestinationBuffer.Driver = driver;
        }

        private static async Task BuildSenderBuffer(Cargo cargo, Guid routeId)
        {
            var sender = await repository.Customers.GetSenderByOrderIdAsync(cargo.OrderId, false);
            var address = sender.Address;
            var truck = await repository.Trucks.GetTruckByRouteIdAsync(routeId, false);
            var driver = truck.Driver.Surname + " "
                + truck.Driver.Name + " "
                + truck.Driver.Patronymic;

            SenderBuffer.LatLng = new PointLatLng(sender.Coordinates.Latitude, sender.Coordinates.Longitude);
            SenderBuffer.Date = (DateTime)cargo.DepartureDate;
            SenderBuffer.Purpose = Purpose.Loading;
            SenderBuffer.Number = cargo.OrderId;
            SenderBuffer.Address = address;
            SenderBuffer.Driver = driver;
        }
    }
}
