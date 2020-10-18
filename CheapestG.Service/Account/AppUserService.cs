using CheapestG.Common.Helper;
using CheapestG.Data.Account;
using CheapestG.Model.Account;
using CheapestG.Model.Model.Account;
using CheapestG.Model.RequestModel;
using CheapestG.Model.ResponseModel;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Service.Account
{
    public interface IAppUserService
    {
        UserLoginResponseModel Login(string username, string password, string deviceToken);
    }
    public class AppUserService : IAppUserService
    {
        private IAppUserRepository _userRepository;
        private IAppUserLoginRepository _appuserloginRepository;
        public AppUserService(IAppUserRepository userRepository, IAppUserLoginRepository appuserloginRepository)
        {
            this._userRepository = userRepository;
            this._appuserloginRepository = appuserloginRepository;
        }
        public UserLoginResponseModel Login(string username, string password, string deviceToken)
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
                },
                LoginTime = DateTime.UtcNow,
                ExpiresTime = DateTime.UtcNow.AddDays(7),
            };
            var userlogin = this._appuserloginRepository.GetSingle(result.UserId);
            if (userlogin == null)
            {
                userlogin = new AppUserLogin();
                userlogin.UserId = result.UserId;
                this._appuserloginRepository.Add(userlogin);
            }
            userlogin.ProviderKey = result.ProviderKey;
            userlogin.LoginTime = result.LoginTime;
            userlogin.ExpiresTime = result.ExpiresTime;
            userlogin.IdFireBase = deviceToken;
            this._appuserloginRepository.Commit();
            return result;
        }
    }
}
