using CheapestG.Context;
using CheapestG.Model.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Account
{
    public interface IAppUserLoginRepository : IRepository<AppUserLogin, string>
    {

    }
    public class AppUserLoginRepository : RepositoryBase<AppUserLogin, string>, IAppUserLoginRepository
    {
        public AppUserLoginRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
    }
