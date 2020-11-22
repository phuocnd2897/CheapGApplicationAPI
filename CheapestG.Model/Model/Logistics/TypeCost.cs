using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Model.Logistics
{
    [Table("TypeCost")]
    public class TypeCost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int CostId { get; set; }
        [ForeignKey("CostId")]
        public virtual Cost Cost { get; set; }
    }
}
