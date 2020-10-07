using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class MappingUser<T>
    {
        public readonly List<HubUserModel> _userconnect = new List<HubUserModel>();
        public int Count
        {
            get
            {
                return _userconnect.Count;
            }
        }
        public void Add(HubUserModel user, string connectionId)
        {
            var item = _userconnect.FirstOrDefault(s => s.UserName == user.UserName);
            if (item == null)
            {
                user.Client = new List<string> { connectionId };
                _userconnect.Add(user);
            }
            else
                item.Client.Add(connectionId);
        }
        public void Remove(string username, string connectionId)
        {
            var item = _userconnect.FirstOrDefault(s => s.UserName == username);
            if (item == null)
            {
                return;
            }
            else
                item.Client.Remove(connectionId);
        }
        public string[] GetByUserName(string username)
        {
            var item = _userconnect.FirstOrDefault(s => s.UserName == username);
            if (item == null)
            {
                return new string[] { };
            }
            else
                return item.Client.ToArray();
        }
        public string[] GetByUserName(string[] username)
        {
            return _userconnect.Where(s => username.Contains(s.UserName)).SelectMany(s => s.Client).ToArray();
        }
    }
}
