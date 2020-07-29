using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using IssueNest.Controllers.Payloads;
using IssueNest.Data;
using IssueNest.Models;
using IssueNest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueNest.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private IssuesDBContext db;
        private IUserAuthService userAuth;
        private IMapper _mapper;

        public UserAuthController(IssuesDBContext db, IUserAuthService userAuth, IMapper mapper)
        {
            this.db = db;
            this.userAuth = userAuth;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserAuthLoginPayload payload)
        {
            if (Request.Cookies.ContainsKey("userId"))
                return StatusCode(403);

            User user = await db.Users.FirstOrDefaultAsync(p => p.Email == payload.Email);
            if (user == null)
                return NotFound();

            // Checking password hash
            if (BCrypt.Net.BCrypt.EnhancedVerify(payload.Password, user.Password))
            {
                if (!userAuth.Login(user.Id))
                    return NoContent();

                CookieOptions cookie = new CookieOptions();
                cookie.HttpOnly = true;
                cookie.Secure = Request.IsHttps;
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Append("userId", $"{user.Id}", cookie);

                return Ok(_mapper.Map<UserReadDTO>(user));
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] UserAuthLogoutPayload payload)
        {
            if (!Request.Cookies.ContainsKey("userId"))
                return StatusCode(403);

            Request.Cookies.TryGetValue("userId", out string _id);
            int id = int.Parse(_id);

            if (payload.Id != id)
                return StatusCode(403);

            userAuth.Logout(id);
            Response.Cookies.Delete("userId");
            return Ok();
        }
    }
}
