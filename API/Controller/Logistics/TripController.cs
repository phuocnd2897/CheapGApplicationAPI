using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheapestG.Service.Logistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller.Logistics
{
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
        public IActionResult GetRoute(string from, string to,int truckId)
        {
            var result = this._tripService.GetRoute(from, to, truckId);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}