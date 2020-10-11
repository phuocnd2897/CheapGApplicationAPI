using CheapestG.Common.Helper;
using CheapestG.Data.Account;
using CheapestG.Model.Account;
using CheapestG.Model.RequestModel;
using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheapestG.Service.Account
{
    public interface IStaffService
    {
        StaffRequestModel CreateNewStaff(StaffRequestModel staffRequestModel, string username);
        StaffRequestModel Get(int Id);
        IEnumerable<StaffResponseModel> GetAll();
        void Delete(int Id);
        SelectResponseModel GetStaffByUsername(string username);
        StaffRequestModel Update(StaffRequestModel staffRequestModel, string username);
    }
    public class StaffService : IStaffService
    {
        protected IStaffRepository _staffRepository;
        protected IAppUserRepository _appUserRepository;
        public StaffService(IStaffRepository staffRepository, IAppUserRepository appUserRepository)
        {
            _staffRepository = staffRepository;
            _appUserRepository = appUserRepository;
        }
        public StaffRequestModel CreateNewStaff(StaffRequestModel staffRequestModel, string username)
        {
            Staff result;
            var user = this._appUserRepository.GetSingle(s => s.UserName.Trim() == staffRequestModel.Username);
            if (user != null)
            {
                result = this._staffRepository.Add(new Staff
                {
                    FirstName = staffRequestModel.FirstName,
                    LastName = staffRequestModel.LastName,
                    Sex = staffRequestModel.Sex,
                    BirthDate = staffRequestModel.BirthDate,
                    PhoneNumber = staffRequestModel.PhoneNumber,
                    Address = staffRequestModel.Address,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Lock = true,
                    CreatedUser = username,
                    UserId = user.UserId
                });
            }
            else
            {
                user = this._appUserRepository.Add(new AppUser
                {
                    UserName = staffRequestModel.Username,
                    PassWord = IdentytiHelper.HashPassword(staffRequestModel.Password),
                    RoleId = staffRequestModel.RoleId
                });
                result = this._staffRepository.Add(new Staff
                {
                    FirstName = staffRequestModel.FirstName,
                    LastName = staffRequestModel.LastName,
                    Sex = staffRequestModel.Sex,
                    BirthDate = staffRequestModel.BirthDate,
                    PhoneNumber = staffRequestModel.PhoneNumber,
                    Address = staffRequestModel.Address,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Lock = true,
                    CreatedUser = username,
                    UserId = user.UserId
                });
            }
            this._staffRepository.Commit();
            staffRequestModel.Id = result.Id;
            return staffRequestModel;
        }

        public void Delete(int Id)
        {
            var result = this._staffRepository.GetSingle(s => s.Id == Id);
            var user = this._appUserRepository.GetSingle(s => s.UserId == result.UserId);
            user.Lock = false;
            this._appUserRepository.Update(user);
            this._staffRepository.Delete(result);
            this._staffRepository.Commit();
        }

        public StaffRequestModel Get(int Id)
        {
            var result = this._staffRepository.GetSingle(s => s.Id == Id);
            var user = this._appUserRepository.GetSingle(s => s.UserId == result.UserId);
            return new StaffRequestModel
            {
                Id = result.Id,
                Username = user.UserName,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Sex = result.Sex,
                BirthDate = result.BirthDate,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                Lock = result.Lock,
                RoleId = user.RoleId
            };
        }
        public IEnumerable<StaffResponseModel> GetAll()
        {
            return this._staffRepository.GetAll(new string[] { "AppUser.AppRole" }).Select(s => new StaffResponseModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Sex = s.Sex,
                BirthDate = s.BirthDate,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                Username = s.AppUser.UserName,
                RoleName = s.AppUser.AppRole.Name
            });
        }

        public SelectResponseModel GetStaffByUsername(string username)
        {
            var user = this._appUserRepository.GetSingle(s => s.UserName == username);
            var staff = this._staffRepository.GetMulti(s => s.UserId == user.UserId && s.Lock == true).FirstOrDefault();
            return new SelectResponseModel
            {
                id = staff.Id.ToString(),
                text = staff.LastName + " " + staff.FirstName
            };
        }

        public StaffRequestModel Update(StaffRequestModel staffRequestModel, string username)
        {
            //var result = this._staffRepository.GetSingle(s => s.Id == staffRequestModel.Id);
            //var user = this._appUserRepository.GetSingle(s => s.UserId == result.UserId);
            //if (this._appUserRepository.Contains(s => s.UserName == staffRequestModel.Username))
            //{
            //    if (staffRequestModel.Username == user.UserName)
            //    {
            //        if (staffRequestModel.Lock == false && result.Lock == true && user.Lock == false)
            //        {
            //            throw new Exception("This user is already in use. Please try another use");
            //        }
            //        else
            //        {
            //            result.FirstName = staffRequestModel.FirstName;
            //            result.LastName = staffRequestModel.LastName;
            //            result.Sex = staffRequestModel.Sex;
            //            result.BirthDate = staffRequestModel.BirthDate;
            //            result.Address = staffRequestModel.Address;
            //            result.PhoneNumber = staffRequestModel.PhoneNumber;
            //            result.UpdatedDate = DateTime.Now;
            //            result.Lock = staffRequestModel.Lock;
            //            result.CreatedUser = username;
            //            user.Lock = result.Lock;
            //            this._appUserRepository.Update(user);
            //            this._staffRepository.Update(result);
            //        }
            //    }
            //}
            //else
            //{

            //}
            return staffRequestModel;
        }
    }
}
