using GoogleApi.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class GasStationResponseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? isOpenning { get; set; }
        public Location Location { get; set; }
    }
}
