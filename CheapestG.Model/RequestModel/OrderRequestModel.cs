using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheapestG.Model.RequestModel
{
    public class OrderRequestModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        [MaxLength(50), Required]
        public string ContractCode { get; set; }
        [MaxLength(50), Required]
        public string CustomerName { get; set; }
        [MaxLength(10), Required]
        public string CustomerPhone { get; set; }
        public string TypeSupplier { get; set; }
        public double Mass { get; set; }
        public int Status { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime FinishedDate { get; set; }
    }
}