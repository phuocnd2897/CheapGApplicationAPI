using CheapestG.Context;
using CheapestG.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Account
{
    public interface IAppUserRepository : IRepository<AppUser, string>
    {

    }
    public class AppUserRepository : RepositoryBase<AppUser, string>, IAppUserRepository
    {
        public AppUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
