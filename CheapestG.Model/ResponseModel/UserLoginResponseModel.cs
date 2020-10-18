using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Model.ResponseModel
{
    public class UserLoginResponseModel
    {
        public string UserId { get; set; }
        public string ProviderKey { get; set; }
        public string Username { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime ExpiresTime { get; set; }
        public string Token { get; set; }
        public SelectResponseModel Roles { get; set; }
    }
}
