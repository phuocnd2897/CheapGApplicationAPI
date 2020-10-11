using CheapestG.Context;
using CheapestG.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Account
{
    public interface IStaffRepository : IRepository<Staff, int>
    {

    }
    public class StaffRepository : RepositoryBase<Staff, int>, IStaffRepository
    {
        public StaffRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
