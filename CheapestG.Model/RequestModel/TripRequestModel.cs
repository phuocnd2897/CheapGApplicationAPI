using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.RequestModel
{
    public class TripRequestModel
    {
        public string Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime StartTime { get; set; }
        public double Mass { get; set; }
        public int TruckId { get; set; }
        public string DriverId { get; set; }
        public RouteResponseModel routeResponseModel { get; set; }
    }
}