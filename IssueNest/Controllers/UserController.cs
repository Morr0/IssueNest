using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IssueNest.Data;
using IssueNest.Models;
using IssueNest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using AutoMapper;

namespace IssueNest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IssuesDBContext db;
        private IUserAuthService userAuth;
        private IMapper mapper;

        public UserController(IssuesDBContext db, IUserAuthService userAuth, IMapper mapper)
        {
            this.db = db;
            this.userAuth = userAuth;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserWriteDto _user)
        {
            User user = mapper.Map<User>(_user);
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password, 11);
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return Ok(mapper.Map<UserReadDTO>(user));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User user = await db.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return user == null ? (IActionResult)NotFound() : Ok(mapper.Map<UserReadDTO>(user));
        }

        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }*/

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, [FromBody] JsonElement payload)
        {
            if (payload.ValueKind == JsonValueKind.Object)
            {
                JsonElement.ObjectEnumerator enumerator = payload.EnumerateObject(); // Calling it twice does not work
                if (enumerator.Count() > 0)
                {
                    User user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
                    if (user == null)
                        return NotFound();

                    foreach (JsonProperty prop in enumerator)
                    {
                        if (prop.Name == "name")
                            user.Name = prop.Value.GetString();
                        if (prop.Name == "email")
                            user.Email = prop.Value.GetString();
                        if (prop.Name == "password")
                            user.Password = prop.Value.GetString();
                    }

                    await db.SaveChangesAsync();

                    return Ok(mapper.Map<UserReadDTO>(user));
                }
            }

            return BadRequest();
        }
    }
}
