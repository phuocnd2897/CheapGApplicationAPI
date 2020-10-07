using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class HubUserModel
    {
        public string UserName { get; set; }
        public List<string> Client { get; set; }
    }
}
