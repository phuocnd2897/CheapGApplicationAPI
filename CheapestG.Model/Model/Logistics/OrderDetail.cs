using CheapestG.Model.Account;
using CheapestG.Model.Logistics;
using CheapestG.Model.Model.Logistics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Logistics
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public double Mass { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string OrderId { get; set; }
        public int TruckId { get; set; }
        [MaxLength(128), Required]
        public string DriverId { get; set; }
        public string CreatedUser { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("TruckId")]
        public virtual Truck Truck { get; set; }
        [ForeignKey("DriverId")]
        public virtual AppUser AppUser { get; set; }
    }
}
