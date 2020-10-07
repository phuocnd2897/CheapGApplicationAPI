using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheapestG.Model.RequestModel
{
    public class StaffRequestModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required, MaxLength(128)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDate { get; set; }
        [Required, MaxLength(200)]
        public string Address { get; set; }
        [Required, MaxLength(10)]
        public string PhoneNumber { get; set; }
        public bool Lock { get; set; }
        public int RoleId { get; set; }
    }
}
