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
        IEnumerable<SelectResponseModel> GetSelectTruck();
    }
    public class TruckRepository : RepositoryBase<Truck, int>, ITruckRepository
    {
        private CheapestGContext dbContext;
        public TruckRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            dbContext = unitOfWork.dbContext;
        }

        public IEnumerable<SelectResponseModel> GetSelectTruck()
        {
            var result = from truck in dbContext.Trucks
                         join trip in dbContext.Trips on truck.Id equals trip.TruckId into _trip
                         from trip in _trip.DefaultIfEmpty()
                         join user in dbContext.AppUsers on truck.Driver equals user.UserId
                         where trip.Status != 1 || trip.Status != 2
                         select new SelectResponseModel
                         {
                             id = truck.Id.ToString(),
                             text = truck.LicensePlates + " - " + truck.Weight + " - " + user.UserName
                         };
            return result;

        }
    }
}
