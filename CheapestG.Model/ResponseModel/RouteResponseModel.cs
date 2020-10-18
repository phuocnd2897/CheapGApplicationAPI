using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class RouteResponseModel
    {
        public Route route { get; set; }
        public double estimatedCost { get; set; }
        public IEnumerable<Location> placesRefuel { get; set; }
        public IEnumerable<GasStationResponseModel> gasStationResponseModels { get; set; }
    }
}