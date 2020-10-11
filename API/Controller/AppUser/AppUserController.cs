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
    public class AppUserController : ControllerBase
    {
        
    }
}
