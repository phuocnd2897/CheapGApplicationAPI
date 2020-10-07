using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class StaffResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}
