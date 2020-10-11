using CheapestG.Data.Logistics;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.Directions.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Service.Logistics
{
    public interface ITripService
    {
        void GetRoute(string from, string to);
    }
    public class TripService : ITripService
    {
        private ITripRepository _tripRepository;
        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public void GetRoute(string from, string to)
        {
            var request = new DirectionsRequest
            {
                Key = "",
                Origin = new Location(from),
                Destination = new Location(to),
                Alternatives = true,
                TravelMode = TravelMode.Driving,
            };

            var result = GoogleMaps.Directions.Query(request);
        }
    }
}
