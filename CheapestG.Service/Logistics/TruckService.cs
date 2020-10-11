using CheapestG.Data.Account;
using CheapestG.Data.Logistics;
using CheapestG.Model.Model.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheapestG.Service.Logistics
{
    public interface ITruckService
    {
        Truck Get(int Id);
        IEnumerable<Truck> GetAll();
        Truck Add(Truck newItem, string username);
        Truck Update(Truck newItem, string username);
        void Delete(int id);
    }
    public class TruckService : ITruckService
    {
        private ITruckRepository _truckRepository;
        public TruckService(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }
        public Truck Add(Truck newItem, string username)
        {
            newItem.CreatedUser = username;
            newItem.CreatedDate = DateTime.Now;
            newItem.UpdatedDate = DateTime.Now;
            var result = this._truckRepository.Add(newItem);
            this._truckRepository.Commit();
            return result;
        }

        public void Delete(int id)
        {
            this._truckRepository.Delete(id);
            this._truckRepository.Commit();
        }

        public Truck Get(int Id)
        {
            return this._truckRepository.GetSingle(s => s.Id == Id);
        }

        public IEnumerable<Truck> GetAll()
        {
            return this._truckRepository.GetAll().OrderBy(s => s.Id);
        }

        public Truck Update(Truck newItem, string username)
        {
            var result = this._truckRepository.GetSingle(s => s.Id == newItem.Id);
            if (result == null)
            {
                return null;
            } else
            {
                result.LicensePlates = newItem.LicensePlates;
                result.Name = newItem.Name;
                result.Brand = newItem.Brand;
                result.Weight = newItem.Weight;
                result.FuelConsumption = newItem.FuelConsumption;
                result.GasTank = newItem.GasTank;
                result.Lock = newItem.Lock;
                result.UpdatedDate = DateTime.Now;
                result.CreatedUser = username;
                this._truckRepository.Update(newItem);
                this._truckRepository.Commit();
            }
            return result;
        }
    }
}
