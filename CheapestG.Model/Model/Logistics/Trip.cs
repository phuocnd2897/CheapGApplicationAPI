using CheapestG.Model.Account;
using CheapestG.Model.Logistics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Logistics
{
    [Table("Trip")]
    public class Trip
    {
        public Trip()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public DateTime EstimatedTime { get; set; }
        public DateTime RealTime { get; set; }
        public float EstimatedCost { get; set; }
        public float CostOfTrip { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int TruckId { get; set; }
        public int DriverId { get; set; }
        [ForeignKey("TruckId")]
        public virtual Truck Truck { get; set; }
        [ForeignKey("DriverId")]
        public virtual Staff Staff { get; set; }
    }
}
