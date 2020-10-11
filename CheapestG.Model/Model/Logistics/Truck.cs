using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Model.Logistics
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
        public float Weight { get; set; }
        public float FuelConsumption { get; set; }
        public float GasTank { get; set;}
        [MaxLength(50)]
        public string Driver { get; set; }
        public bool Lock { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedUser { get; set; }

    }
}
