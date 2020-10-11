using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheapestG.Model.RequestModel;
using CheapestG.Service.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller.AppUser
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        public IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [HttpPost]
        public IActionResult CreateNewStaff(StaffRequestModel staffRequestModel)
        {
            try
            {
                string username = User.Identity.Name;
                var result = this._staffService.CreateNewStaff(staffRequestModel, username);
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
                var result = this._staffService.Get(Id);
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
        public IActionResult GetAll ()
        {
            try
            {
                var result = this._staffService.GetAll();
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("An error occurred. Please try again");
            }
        }
        [HttpDelete]
        public IActionResult Delete([FromQuery] int Id)
        {
            try
            {
                this._staffService.Delete(Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
