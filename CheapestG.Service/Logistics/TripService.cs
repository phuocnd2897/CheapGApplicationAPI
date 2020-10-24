using CheapestG.Common.Constant;
using CheapestG.Data.Account;
using CheapestG.Data.Logistics;
using CheapestG.Model.Logistics;
using CheapestG.Model.Model.Account;
using CheapestG.Model.RequestModel;
using CheapestG.Model.ResponseModel;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Places.Search.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheapestG.Service.Logistics
{
    public interface ITripService
    {
        IEnumerable<RouteResponseModel> GetRoute(string from, string to, int truckId, float weight);
        TripRequestModel Add(TripRequestModel item, string username);
        RouteResponseModel GetSpecifyRoute();
        Trip UpdateStatus(string id, int status);
        void CheckNoti();
    }
    public class TripService : ITripService
    {
        private ITripRepository _tripRepository;
        private ITruckRepository _truckRepository;
        private IAppUserLoginRepository _appUserLoginRepository;
        public TripService(ITripRepository tripRepository, ITruckRepository truckRepository, IAppUserLoginRepository appUserLoginRepository)
        {
            _tripRepository = tripRepository;
            _truckRepository = truckRepository;
            _appUserLoginRepository = appUserLoginRepository;
        }

        public TripRequestModel Add(TripRequestModel item, string username)
        {
            var truck = _truckRepository.GetMulti(s => s.Id == item.TruckId, new string[] { "AppUser" }).FirstOrDefault();
            var result = this._tripRepository.Add(new Trip
            {
                Origin = item.routeResponseModel.route.Legs.FirstOrDefault().StartAddress,
                Destination = item.routeResponseModel.route.Legs.FirstOrDefault().EndAddress,
                StartTime = item.StartTime,
                EstimatedTime = TimeSpan.FromSeconds(item.routeResponseModel.route.Legs.FirstOrDefault().Duration.Value).ToString(@"hh\:mm\:ss\:f"),
                EstimatedCost = item.routeResponseModel.estimatedCost,
                Mass = item.Mass,
                Status = StatusTripConst.Pending,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TruckId = item.TruckId,
                DriverId = truck.AppUser.UserId,
                CreatedUser = username
            });
            if (result != null)
            {
                item.Id = result.Id;
                var userLogin = this._appUserLoginRepository.GetMulti(s => s.UserId == truck.AppUser.UserId).OrderByDescending(s => s.LoginTime).FirstOrDefault();
                String tittle = "You receive a trip";
                String body = "Let check and start your trip";
                var check = this.NotifyAsync(userLogin.IdFireBase, tittle, body).Result;
                if (check)
                {
                    string jsonData = JsonConvert.SerializeObject(item.routeResponseModel);
            System.IO.File.WriteAllText("route.json", jsonData);

            this._appUserLoginRepository.Commit();
            return item;
        }
    }
            return null;
        }

        public void CheckNoti()
        {
            var userLogin = this._appUserLoginRepository.GetMulti(s => s.UserId == "34e41037-058d-4c32-9e25-1a9920236251").OrderByDescending(s => s.LoginTime).FirstOrDefault();
            String tittle = "You receive a trip";
            String body = "Let check and start your trip";
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAN3NZffM:APA91bEA3fAlOU2nGpFra_SOsq22uVm5CzXZZsh3p4JVHgf0zNOj4dZnXL1lbtAtOY9qgNQyYEMWyuweEGvFc-9zANlbflT_xBs4FjZR8PPrkifFs29pU4fF4E7s26-joYLQbyP0orPD");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "238158446067");

                var data = new
                {
                    to = userLogin.IdFireBase, // Recipient device token
                    notification = new { tittle, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = httpClient.SendAsync(httpRequest).Result;

                        if (result.IsSuccessStatusCode)
                        {
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public IEnumerable<RouteResponseModel> GetRoute(string from, string to, int truckId, float weight)
        {
            var truck = _truckRepository.GetSingle(s => s.Id == truckId, new string[] { "Gas" });
            double distanceRefuel = 0;
            double distance = 0;
            if (!_tripRepository.Contains(s => s.TruckId == truckId))
            {
                distanceRefuel = ((truck.GasTank * 100) / ((weight * truck.FuelConsumption) / truck.Weight) - 30) * 1000;
            }
            else
            {

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
            int count = 0;
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
                double estimatedCost = truck.Gas.Price * placeRefuel.Count();
                routeResponseModel.Add(new RouteResponseModel
                {
                    estimatedCost = estimatedCost,
                    placesRefuel = placeRefuel,
                    route = route,
                    gasStationResponseModels = gasStationResponseModels
                });
            }
            return routeResponseModel;
        }

        public RouteResponseModel GetSpecifyRoute()
        {
            using (StreamReader r = new StreamReader("route.json"))
            {
                string json = r.ReadToEnd();
                RouteResponseModel item = JsonConvert.DeserializeObject<RouteResponseModel>(json);
                return item;
            }
        }

        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAN3NZffM:APA91bEA3fAlOU2nGpFra_SOsq22uVm5CzXZZsh3p4JVHgf0zNOj4dZnXL1lbtAtOY9qgNQyYEMWyuweEGvFc-9zANlbflT_xBs4FjZR8PPrkifFs29pU4fF4E7s26-joYLQbyP0orPD");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "238158446067");

                var data = new
                {
                    to =to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Trip UpdateStatus(string id, int status)
        {
            var result = this._tripRepository.GetSingle(s => s.Id == id);
            result.Status = status;
            this._tripRepository.Update(result);
            this._tripRepository.Commit();
            return result;
        }
    }
}

