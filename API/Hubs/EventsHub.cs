using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    [Authorize("Bearer")]
    public class EventsHub : Hub<IEventsHub>
    {
        public readonly static MappingUser<HubUserModel> _connections = new MappingUser<HubUserModel>();
        public override async Task OnConnectedAsync()
        {
            _connections.Add(new HubUserModel
            {
                UserName = Context.User.Identity.Name
            }, Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string username = Context.User.Identity.Name;
            _connections.Remove(username, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
