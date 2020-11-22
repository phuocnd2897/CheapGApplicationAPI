using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheapestG.Model.Model.Logistics;
using CheapestG.Model.RequestModel;
using CheapestG.Service.Logistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller.Logistics
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private IOrderService _orderService;
        public TripController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("CreateOrder")]
        public IActionResult CreateOrder(List<OrderRequestModel> newItems)
        {
            var username = User.Identity.Name;
            var result = this._orderService.CreateOrder(newItems, username);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateStatus")]
        public IActionResult UpdateStatus(string id, int status)
        {
            try
            {
                var result = this._orderService.UpdateStatus(id, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("CheckNot")]
        public IActionResult CheckNot(string id, int status)
        {
            try
            {
                this._orderService.CheckNoti();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}