using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<IActionResult> LoginUser([FromBody] JsonElement payload)
        {
            Console.WriteLine("Here");
            if (Request.Cookies.ContainsKey("userId"))
                return BadRequest();
            Console.WriteLine("Here");

            JsonElement.ObjectEnumerator enumerator = payload.EnumerateObject();
            if (enumerator.Count() > 1)
            {
                Console.WriteLine(payload.GetRawText());
                // Getting json payload (email, password)
                string email = null, password = null;
                foreach (JsonProperty prop in enumerator)
                {
                    if (prop.Name == "email")
                        email = prop.Value.GetString();
                    if (prop.Name == "password")
                        password = prop.Value.GetString();
                }

                if (email != null && password != null)
                {
                    User user = await db.Users.FirstOrDefaultAsync(p => p.Email == email);
                    if (user == null)
                        return NotFound();

                    // Checking password hash
                    if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
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
            }

            return BadRequest();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (!Request.Cookies.ContainsKey("userId"))
                return Unauthorized();

            Request.Cookies.TryGetValue("userId", out string _id);
            int id = int.Parse(_id);

            userAuth.Logout(id);
            Response.Cookies.Delete("userId");
            return Ok();
        }
    }
}
