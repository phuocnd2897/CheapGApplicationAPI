using CheapestG.Model.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CheapestG.Model.Model.Account
{
    [Table("AppUserLogin")]
    public class AppUserLogin
    {
        [Key]
        [MaxLength(128)]
        public string UserId { get; set; }
        [Required, MaxLength(128)]
        public string ProviderKey { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        [Required]
        public DateTime ExpiresTime { get; set; }
        [Required, MaxLength(200)]
        public string IdFireBase { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
