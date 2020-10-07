using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Account
{
    [Table("User")]
    public class AppUser
    {
        public AppUser()
        {
            UserId = Guid.NewGuid().ToString();
        }
        [Key]
        [MaxLength(128)]
        public string UserId { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(128)]
        public string PassWord { get; set; }
        public bool Lock { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }
    }
}
