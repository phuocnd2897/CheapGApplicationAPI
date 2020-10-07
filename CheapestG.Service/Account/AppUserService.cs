using CheapestG.Common.Helper;
using CheapestG.Data.Account;
using CheapestG.Model.Account;
using CheapestG.Model.RequestModel;
using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Service.Account
{
    public interface IAppUserService
    {
        UserLoginResponseModel Login(string username, string password);
    }
    public class AppUserService : IAppUserService
    {
        private IAppUserRepository _userRepository;
        public AppUserService(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserLoginResponseModel Login(string username, string password)
        {
            var appuser = this._userRepository.GetSingle(s => s.UserName == username, new string[] { "AppRole" });
            if (appuser == null || !IdentytiHelper.VerifyHashedPassword(appuser.PassWord, password) || appuser.Lock == true)
                return null;
            var result = new UserLoginResponseModel()
            {
                UserId = appuser.UserId,
                Username = appuser.UserName,
                ProviderKey = Guid.NewGuid().ToString(),
                Roles = new SelectResponseModel
                {
                    id = appuser.AppRole.Id.ToString(),
                    text = appuser.AppRole.Name
                }
            };
            return result;
        }
    }
}
