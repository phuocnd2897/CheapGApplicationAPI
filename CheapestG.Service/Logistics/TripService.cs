using CheapestG.Data.Logistics;
using CheapestG.Model.ResponseModel;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Places.Search.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CheapestG.Service.Logistics
{
    public interface ITripService
    {
        IEnumerable<RouteResponseModel> GetRoute(string from, string to, int truckId);

    }
    public class TripService : ITripService
    {
        private ITripRepository _tripRepository;
        private ITruckRepository _truckRepository;
        public TripService(ITripRepository tripRepository, ITruckRepository truckRepository)
        {
            _tripRepository = tripRepository;
            _truckRepository = truckRepository;
        }
        public IEnumerable<RouteResponseModel> GetRoute(string from, string to, int truckId)
        {
            var truck = _truckRepository.GetSingle(s => s.Id == truckId);
            double distanceRefuel = 0;
            double distance = 0;
            if (!_tripRepository.Contains(s => s.TruckId == truckId))
            {
                distanceRefuel = ((truck.GasTank * 100) / truck.FuelConsumption - 30)*1000;
            }
            var requestDirection = new DirectionsRequest
            {
                Key = "AIzaSyAo421sSPdh65qh2f0B08C2U4eU5-pGg4c",
                Origin = new Location(from),
                Destination = new Location(to),
                Alternatives = true,
                TravelMode = TravelMode.Driving,
                Language = GoogleApi.Entities.Common.Enums.Language.Vietnamese
            };
            var resultDirection = GoogleMaps.Directions.Query(requestDirection);
            List<RouteResponseModel> routeResponseModel = new List<RouteResponseModel>();
            List<GasStationResponseModel> gasStationResponseModels = null;
            List<Location> placeRefuel = null;
            float km = 0;
            foreach (var route in resultDirection.Routes)
            {
                placeRefuel= new List<Location>();
                gasStationResponseModels = new List<GasStationResponseModel>();
                foreach (var leg in route.Legs)
                {
                    foreach (var step in leg.Steps)
                    {
                        distance += step.Distance.Value;
                        if (distance > distanceRefuel)
                        {
                            placeRefuel.Add(step.StartLocation);
                            var requestNearBy = new GoogleApi.Entities.Places.Search.NearBy.Request.PlacesNearBySearchRequest
                            {
                                Key = "AIzaSyAo421sSPdh65qh2f0B08C2U4eU5-pGg4c",
                                Location = new GoogleApi.Entities.Places.Search.NearBy.Request.Location
                                {
                                    Latitude = step.StartLocation.Latitude,
                                    Longitude = step.StartLocation.Longitude,
                                },
                                Radius = 5000,
                                Type = SearchPlaceType.GasStation,
                                Language = GoogleApi.Entities.Common.Enums.Language.Vietnamese

                            };
                            var responseNearBy = GooglePlaces.NearBySearch.QueryAsync(requestNearBy).Result;
                            foreach (var station in responseNearBy.Results)
                            {
                                gasStationResponseModels.Add(new GasStationResponseModel
                                {
                                    Name = station.Name,
                                    Location = station.Geometry.Location,
                                    Address = station.Vicinity,
                                    isOpenning = station.OpeningHours?.OpenNow
                                });
                            }
                            distance = step.Distance.Value;
                        };
                    }
                    
                }
                routeResponseModel.Add(new RouteResponseModel
                {
                    estimatedCost = 0,
                    placesRefuel = placeRefuel,
                    route = route,
                    gasStationResponseModels = gasStationResponseModels
                });
            }
            return routeResponseModel;
        }
    }
}

