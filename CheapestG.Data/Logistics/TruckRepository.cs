using CheapestG.Context;
using CheapestG.Model.Logistics;
using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheapestG.Data.Logistics
{
    public interface ITruckRepository : IRepository<Truck, int>
    {
        IEnumerable<TruckResponseModel> GetSelectTruck();
    }
    public class TruckRepository : RepositoryBase<Truck, int>, ITruckRepository
    {
        private CheapestGContext dbContext;
        public TruckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            dbContext = unitOfWork.dbContext;
        }

        public IEnumerable<TruckResponseModel> GetSelectTruck()
        {
            var result = from truck in dbContext.Trucks
                         join trip in dbContext.OrderDetails on truck.Id equals trip.TruckId into _trip
                         from trip in _trip.DefaultIfEmpty()
                         join user in dbContext.AppUsers on truck.Driver equals user.UserId
                         join staff in dbContext.Staffs on user.UserId equals staff.UserId
                         where trip.Status != 1 || trip.Status != 2
                         select new TruckResponseModel
                         {
                             LicensePlate = truck.LicensePlates,
                             Weight = truck.Weight,
                             DriverId = truck.Driver,
                             DriverName = staff.LastName + " " + staff.FirstName
                         };
            return result.OrderByDescending(s => s.Weight);

        }
    }
}
