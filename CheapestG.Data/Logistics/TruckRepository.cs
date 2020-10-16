using CheapestG.Context;
using CheapestG.Model.Logistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Logistics
{
    public interface ITruckRepository : IRepository<Truck, int>
    {

    }
    public class TruckRepository : RepositoryBase<Truck, int>, ITruckRepository
    {
        public TruckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
