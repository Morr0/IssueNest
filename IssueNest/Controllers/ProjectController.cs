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

namespace IssueNest.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private IssuesDBContext db;
        public ProjectController(IssuesDBContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject()
        {
            if (Request.Headers.ContainsKey("user_id") && Request.Headers.ContainsKey("name"))
            {
                Request.Headers.TryGetValue("user_id", out var _userId);
                Request.Headers.TryGetValue("name", out var name);

                // Validate an int
                if (!Int32.TryParse(_userId, out int userId))
                    return BadRequest();

                // Check if the user exists
                if (await db.Users.FindAsync(userId) == null)
                    return Unauthorized();

                // Make a new project
                await db.Projects.AddAsync(new Project
                {
                    UserId = userId,
                    Name = name,
                    Hook = Guid.NewGuid().ToString(),
                });
                await db.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }
    }
}
