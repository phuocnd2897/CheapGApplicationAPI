using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheapestG.Model.Model.Logistics;
using CheapestG.Service.Logistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller.Logistics
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        public ITruckService _TruckService;
        public TruckController(ITruckService TruckService)
        {
            _TruckService = TruckService;
        }
        [HttpPost]
        public IActionResult CreateNewTruck(Truck newItem)
        {
            try
            {
                string username = User.Identity.Name;
                var result = this._TruckService.Add(newItem, username);
                if (result != null)
                {
                    return Ok(result);
                };
                return BadRequest("Co loi xay ra");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get([FromQuery] int Id)
        {
            try
            {
                var result = this._TruckService.Get(Id);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred. Please try again");
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var result = this._TruckService.GetAll();
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("An error occurred. Please try again");
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] int Id)
        {
            try
            {
                this._TruckService.Delete(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
