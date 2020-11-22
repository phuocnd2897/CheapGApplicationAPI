using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class OrderResponseModel
    {
        public string ContractCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public IEnumerable<OrderDetailResponseModel> orderDetailResponseModel { get; set; }


    }
}
