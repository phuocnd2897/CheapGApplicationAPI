using CheapestG.Common.Constant;
using CheapestG.Data.Account;
using CheapestG.Data.Logistics;
using CheapestG.Model.Logistics;
using CheapestG.Model.Model.Account;
using CheapestG.Model.Model.Logistics;
using CheapestG.Model.RequestModel;
using CheapestG.Model.ResponseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CheapestG.Service.Logistics
{
    public interface IOrderService
    {
        IEnumerable<OrderResponseModel> CreateOrder(IEnumerable<OrderRequestModel> newItems, string username);
        Order UpdateStatus(string id, int status);
        void CheckNoti();
    }
    public class TripService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private ITruckRepository _truckRepository;
        private IAppUserLoginRepository _appUserLoginRepository;
        public TripService(IOrderRepository orderRepository, ITruckRepository truckRepository, IAppUserLoginRepository appUserLoginRepository)
        {
            _orderRepository = orderRepository;
            _truckRepository = truckRepository;
            _appUserLoginRepository = appUserLoginRepository;
        }


        public void CheckNoti()
        {
            var userLogin = this._appUserLoginRepository.GetMulti(s => s.UserId == "34e41037-058d-4c32-9e25-1a9920236251").OrderByDescending(s => s.LoginTime).FirstOrDefault();
            String tittle = "You receive a order";
            String body = "Let check and start your order";
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAN3NZffM:APA91bEA3fAlOU2nGpFra_SOsq22uVm5CzXZZsh3p4JVHgf0zNOj4dZnXL1lbtAtOY9qgNQyYEMWyuweEGvFc-9zANlbflT_xBs4FjZR8PPrkifFs29pU4fF4E7s26-joYLQbyP0orPD");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "238158446067");

                var data = new
                {
                    to = userLogin.IdFireBase, // Recipient device token
                    notification = new { tittle, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = httpClient.SendAsync(httpRequest).Result;

                        if (result.IsSuccessStatusCode)
                        {
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public IEnumerable<OrderResponseModel> CreateOrder(IEnumerable<OrderRequestModel> newItems, string username)
        {
            double mass = 0;
            List<OrderDetailResponseModel> orderDetails = new List<OrderDetailResponseModel>();
            List<OrderResponseModel> orders = new List<OrderResponseModel>();
            var trucks = this._truckRepository.GetSelectTruck();
            if (newItems.Sum(s => s.Mass) <= trucks.ElementAt(0).Weight)
            {

                var truck = trucks.Where(s => (s.Weight - newItems.Sum(s => s.Mass)) > 0).OrderBy(s => s.Weight - newItems.Sum(s => s.Mass)).FirstOrDefault();
                foreach (var item in newItems)
                {
                   var order = this._orderRepository.Add(new Order
                    {
                        Origin = item.Origin,
                        Destination = item.Destination,
                        ContractCode = item.ContractCode,
                        CustomerName = item.CustomerName,
                        CustomerPhone = item.CustomerPhone,
                        TypeSupplier = item.TypeSupplier,
                        Mass = item.Mass,
                        Status = StatusTripConst.Pending,
                        DepartureDate = item.DepartureDate,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedUser = username
                    });
                    orderDetails.Add(new OrderDetailResponseModel
                    {
                        Mass = item.Mass,
                        DriverId = truck.DriverId,
                        DriverName = truck.DriverName,
                        LicensePlate = truck.LicensePlate,
                        Weight = truck.Weight,
                        OrderId = order.Id
                    });
                    orders.Add(new OrderResponseModel
                    {
                        ContractCode = order.ContractCode,
                        CustomerName = order.CustomerName,
                        CustomerPhone = order.CustomerPhone,
                        Origin = order.Origin,
                        Destination = order.Destination,
                        orderDetailResponseModel = orderDetails
                    });
                }
            }
            else
            {
                string temp = "";
                Order order = new Order();
                foreach (var truck in trucks)
                {
                    foreach (var item in newItems.OrderByDescending(s => s.Mass))
                    {
                        
                        if (temp != item.ContractCode)
                        {
                            order = this._orderRepository.Add(new Order
                            {
                                Origin = item.Origin,
                                Destination = item.Destination,
                                ContractCode = item.ContractCode,
                                CustomerName = item.CustomerName,
                                CustomerPhone = item.CustomerPhone,
                                TypeSupplier = item.TypeSupplier,
                                Mass = item.Mass,
                                Status = StatusTripConst.Pending,
                                DepartureDate = item.DepartureDate,
                                FinishedDate = item.DepartureDate,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                                CreatedUser = username
                            });
                        }
                        temp = item.ContractCode;
                        if (truck.Weight != 0)
                        {
                            if (item.Mass != 0)
                            {
                                if (item.Mass < truck.Weight)
                                {

                                    orderDetails.Add(new OrderDetailResponseModel
                                    {
                                        Mass = item.Mass,
                                        DriverId = truck.DriverId,
                                        DriverName = truck.DriverName,
                                        LicensePlate = truck.LicensePlate,
                                        Weight = truck.Weight
                                    });
                                    truck.Weight = truck.Weight - item.Mass;

                                }
                                else
                                {
                                    orderDetails.Add(new OrderDetailResponseModel
                                    {
                                        Mass = truck.Weight,
                                        DriverId = truck.DriverId,
                                        DriverName = truck.DriverName,
                                        LicensePlate = truck.LicensePlate,
                                        Weight = truck.Weight,
                                        OrderId = order.Id
                                    });
                                    item.Mass = item.Mass - truck.Weight;
                                    //if (item.Mass)
                                    //{

                                    //}
                                    break;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }
                        orders.Add(new OrderResponseModel
                        {
                            ContractCode = order.ContractCode,
                            CustomerName = order.CustomerName,
                            CustomerPhone = order.CustomerPhone,
                            Origin = order.Origin,
                            Destination = order.Destination,
                            orderDetailResponseModel = orderDetails
                        });
                    }
                }
            }
            this._orderRepository.Commit();
            return orders;
        }


        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAN3NZffM:APA91bEA3fAlOU2nGpFra_SOsq22uVm5CzXZZsh3p4JVHgf0zNOj4dZnXL1lbtAtOY9qgNQyYEMWyuweEGvFc-9zANlbflT_xBs4FjZR8PPrkifFs29pU4fF4E7s26-joYLQbyP0orPD");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "238158446067");

                var data = new
                {
                    to = to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Order UpdateStatus(string id, int status)
        {
            var result = this._orderRepository.GetSingle(s => s.Id == id);
            result.Status = status;
            this._orderRepository.Update(result);
            this._orderRepository.Commit();
            return result;
        }
    }
}

