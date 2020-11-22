using CheapestG.Context;
using CheapestG.Model.Logistics;
using CheapestG.Model.Model.Logistics;
using CheapestG.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheapestG.Data.Logistics
{
    public interface IOrderRepository : IRepository<Order, string>
    {
    }
    public class OrderRepository : RepositoryBase<Order, string>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
