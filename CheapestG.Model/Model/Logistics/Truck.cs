using CheapestG.Model.Account;
using CheapestG.Model.Model.Logistics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Logistics
{
    [Table("Truck")]
    public class Truck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(9)]
        public string LicensePlates { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Brand { get; set; }
        public double Weight { get; set; }
        public double FuelConsumption { get; set; }
        public double GasTank { get; set;}
        public int GasId { get; set; }
        [MaxLength(128)]
        public string Driver { get; set; }
        public bool Lock { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedUser { get; set; }
        [ForeignKey("Driver")]
        public virtual AppUser AppUser { get; set; }

    }
}
