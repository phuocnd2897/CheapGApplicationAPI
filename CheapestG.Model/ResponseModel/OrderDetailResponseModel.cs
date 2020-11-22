using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class OrderDetailResponseModel
    {
        public double Mass { get; set; }
        public string DriverId { get; set; }
        public string DriverName { get; set; }
        public string LicensePlate { get; set; }
        public double Weight { get; set; }
        public string OrderId { get; set; }
    }
}
