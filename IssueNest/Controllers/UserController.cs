using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IssueNest.Data;
using IssueNest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace IssueNest.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IssuesDBContext db;

        public UserController(IssuesDBContext db)
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

                User user = new User {
                    Name = name,
                    Email = email,
                    Password = password
                };

                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user != null)
            {
                return Ok(new
                {
                    name = user.Name,
                    email = user.Email,
                    timestamp = user.Timestamp
                });
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user != null)
            {
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

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

                    return Ok(new
                    {
                        name = user.Name,
                        email = user.Email,
                        timestamp = user.Timestamp
                    });
                } 
            }

            return BadRequest();
        }
    }
}
