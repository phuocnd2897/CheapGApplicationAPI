using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
    public interface IHubService
    {
        void SendTrip(DateTime ngay, string username);
    }
    public class HubService : IHubService
    {
        public IHubContext<EventsHub, IEventsHub> _hubContext { get; }
        private readonly MappingUser<HubUserModel> _connections = EventsHub._connections;
        public HubService(IHubContext<EventsHub, IEventsHub> hubContext)
        {
            this._hubContext = hubContext;
        }
        public void SendTrip(DateTime dateTime, string username)
        {
            if (dateTime.Date == DateTime.Now.Date)
            {
                var ids = _connections._userconnect.Where(s => s.UserName == username).SelectMany(s => s.Client);
                foreach (string id in ids)
                {
                    this._hubContext.Clients.Client(id).eventSendTrip();
                }
            }
        }
    }
}
