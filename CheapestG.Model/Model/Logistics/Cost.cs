using CheapestG.Model.Logistics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Model.Logistics
{
    [Table("Cost")]
    public class Cost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Description { get; set; }
        public double Price { get; set; }
        public int TypeCost { get; set; }
        public string OrderDetailId { get; set; }
        [ForeignKey("OrderDetailId")]
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
