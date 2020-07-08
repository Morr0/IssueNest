using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueNest.Data;
using IssueNest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueNest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IDataProvider db;

        public UserController(IDataProvider db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            if (Request.Headers.ContainsKey("name") && Request.Headers.ContainsKey("email") 
                && Request.Headers.ContainsKey("password"))
            {
                Request.Headers.TryGetValue("name", out var name);
                Request.Headers.TryGetValue("email", out var email);
                Request.Headers.TryGetValue("password", out var password);

                User user = new User{
                    Name = name,
                    Email = email,
                    Password = password
                };

                if (await db.CreateNewUser(user))
                    return Ok();

                return StatusCode(500);
            }

            return BadRequest();
        }
    }
}
