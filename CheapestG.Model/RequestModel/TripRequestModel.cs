using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.RequestModel
{
    public class TripRequestModel
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public double Mass { get; set; }
        public int TruckId { get; set; }
        public RouteResponseModel routeResponseModel { get; set; }
    }
}