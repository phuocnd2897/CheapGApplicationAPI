using CheapestG.Data.Logistics;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Service.Logistics
{
    public interface ITripService
    {
        DirectionsResponse GetRoute(string from, string to);
    }
    public class TripService : ITripService
    {
        private ITripRepository _tripRepository;
        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public DirectionsResponse GetRoute(string from, string to)
        {
            var request = new DirectionsRequest
            {
                Key = "AIzaSyAo421sSPdh65qh2f0B08C2U4eU5-pGg4c",
                Origin = new Location(from),
                Destination = new Location(to),
                Alternatives = true,
                TravelMode = TravelMode.Driving,
            };
            var result = GoogleMaps.Directions.Query(request);

            return result;
        }
    }
}
