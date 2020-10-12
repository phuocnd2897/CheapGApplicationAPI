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
        public IActionResult GetRoute(string from, string to)
        {
            var result = this._tripService.GetRoute(from, to);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
