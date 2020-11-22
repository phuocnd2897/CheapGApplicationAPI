using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Model.Logistics
{
    [Table("Order")]
    public class Order
    {
        public Order()
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
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedUser { get; set; }

    }
}
