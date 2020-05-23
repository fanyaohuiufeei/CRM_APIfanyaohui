using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BT.API.Controllers
{
    [Route("api/texts")]
    [ApiController]
    public class TextsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTokenData()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            return Ok(User.Identity.Name);
        }

        [HttpGet("admin")]
        [Authorize(Roles ="管理员")]
        public IActionResult Admin()
        {
            return Ok("我是管理员");
        }

        [HttpGet("yg")]
        [Authorize(Roles = "员工,管理员")]
        public IActionResult Yg()
        {
            return Ok("我是普通员工");
        }

    }
}