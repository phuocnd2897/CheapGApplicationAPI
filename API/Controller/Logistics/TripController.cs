using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }
        [HttpGet]
        [Route("GetRoute")]
        public IActionResult GetRoute(string from, string to,int truckId, float weight)
        {
            var result = this._tripService.GetRoute(from, to, truckId, weight);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetSpecifyRoute")]
        public IActionResult GetSpecifyRoute()
        {
            var result = this._tripService.GetSpecifyRoute();
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add(TripRequestModel item)
        {
            try
            {
                var username = User.Identity.Name;
                var result = this._tripService.Add(item, username);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateStatus")]
        public IActionResult UpdateStatus(string id, int status)
        {
            try
            {
                var result = this._tripService.UpdateStatus(id, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}