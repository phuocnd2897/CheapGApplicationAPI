using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheapestG.Model.RequestModel
{
    public class UserLoginRequestModel
    {
        [Required, MaxLength(50), MinLength(5)]
        public string Username { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string Password { get; set; }
        public string DeviceToken { get; set; }
    }
}
