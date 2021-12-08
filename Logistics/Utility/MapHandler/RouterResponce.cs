using GMap.NET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Utility.MapHandler
{
    public class RouterResponce
    {
        public class Geometry
        {
            public List<List<double>> coordinates { get; set; }
            public string type { get; set; }
        }

        public class Route
        {
            public Geometry geometry { get; set; }
        }

        public class Root
        {
            public List<Route> routes { get; set; }

            public async Task<List<PointLatLng>> WaypointsToLatLng()
            {
                var list = new List<PointLatLng>();

                foreach (var elem in routes[0].geometry.coordinates)
                    await AddToList(list, elem);

                return list;
            }

            private static async Task AddToList(List<PointLatLng> list, List<double> elem)
            {
                await Task.Run(() =>
                {
                    var point = new PointLatLng(elem[1], elem[0]);
                    list.Add(point);
                });
            }
        }
    }
}
