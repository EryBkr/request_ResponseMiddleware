using LoggingTestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            var user = new UserLoginResponseModel
            {
                Success = true,
                UserEmail = "test@mail.com"
            };

            return Ok(user);
        }

        [HttpPost]
        [Route("login-only")]
        public async Task<IActionResult> LoginOnly([FromBody] UserLoginRequestModel model)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestModel model)
        {
            var user = new UserLoginResponseModel
            {
                Success = false,
                UserEmail = "test_1@mail.com"
            };

            return Ok(user);
        }
    }
}
