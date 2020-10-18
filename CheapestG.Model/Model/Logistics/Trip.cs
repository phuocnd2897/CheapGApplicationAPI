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
        [MaxLength(200), Required]
        public string Origin { get; set; }
        [MaxLength(200), Required]
        public string Destination { get; set; }
        public DateTime StarTime { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public TimeSpan? RealTime { get; set; }
        public double EstimatedCost { get; set; }
        public double? CostOfTrip { get; set; }
        public int Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int TruckId { get; set; }
        [MaxLength(128), Required]
        public string DriverId { get; set; }

        public string CreatedUser { get; set; }
        [ForeignKey("TruckId")]
        public virtual Truck Truck { get; set; }
        [ForeignKey("DriverId")]
        public virtual AppUser AppUser { get; set; }
    }
}
