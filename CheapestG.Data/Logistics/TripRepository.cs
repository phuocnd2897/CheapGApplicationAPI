using CheapestG.Context;
using CheapestG.Model.Model.Logistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Logistics
{
    public interface ITripRepository : IRepository<Trip, string>
    {

    }
    public class TripRepository : RepositoryBase<Trip, string>, ITripRepository
    {
        protected TripRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
