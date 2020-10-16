using CheapestG.Context;
using CheapestG.Model.Logistics;
using CheapestG.Model.ResponseModel;
using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Places.Search.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Logistics
{
    public interface ITripRepository : IRepository<Trip, string>
    {
        IEnumerable<GasStationResponseModel> GetGasStation(Step step);
    }
    public class TripRepository : RepositoryBase<Trip, string>, ITripRepository
    {
        public TripRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IEnumerable<GasStationResponseModel> GetGasStation(Step step)
        {
            List<GasStationResponseModel> gasStationResponseModels = new List<GasStationResponseModel>();
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
                    Address = "",
                    isOpenning = station.OpeningHours?.OpenNow
                });
            }
            return gasStationResponseModels;
        }
    }
}
