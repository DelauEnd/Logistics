using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static Logistics.Utility.MapHandler.RouterResponce;

namespace Logistics.Utility.MapHandler
{ 
    public class RequestHandler
    {
        private WebClient WebClient{ get; set; }
        private readonly string RequestPath = "https://routing.openstreetmap.de/routed-car/route/v1/driving/";
        private readonly string RequestParams = "?geometries=geojson&overview=full&alternatives=false&steps=true";

        public RequestHandler()
        {
            WebClient = new WebClient();
            WebClient.Encoding = Encoding.UTF8;
            WebClient.UseDefaultCredentials = true;
        }

        public Root GetRouteByHttp(string pointsParams)
        {
            var request = RequestPath + pointsParams + RequestParams;

            var result = WebClient.DownloadString(request);
            Root route = JsonConvert.DeserializeObject<Root>(result);

            return route;
        }
    }
}
