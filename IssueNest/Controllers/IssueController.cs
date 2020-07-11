using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IssueNest.Data;
using IssueNest.Models;
using IssueNest.Services;
using IssueNest.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueNest.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private IssuesDBContext db;
        private IssuesManager manager;

        public IssueController(IssuesDBContext db)
        {
            this.db = db;
            //this.manager = manager;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetIssues(int projectId)
        {
            if (await db.Projects.FindAsync(projectId) == null)
                return NotFound();

            List<Issue> list = await db.Issues.Where(p => p.ProjectId == projectId)
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost("{projectId}")]
        public async Task<IActionResult> CreateIssue(int projectId)
        {
            if (await db.Projects.FindAsync(projectId) == null)
                return NotFound();

            try
            {
                //Request.Headers.TryGetValue("from", out var _from); // NOT IMPORTANT FOR NOW
                Request.Headers.TryGetValue("description", out var description);
                Request.Headers.TryGetValue("type", out var _type);

                Enum.TryParse(_type.ToString().ToUpper(), out IssueType type);

                await db.Issues.AddAsync(new Issue
                {
                    ProjectId = projectId,
                    Description = description.ToString(),
                    IssueFrom = IssueFrom.GITHUB.ToString(),
                    IssueState = IssueState.EXISTING.ToString(),
                    IssueType = type.ToString(),
                });
                await db.SaveChangesAsync();

                return Ok();
            } catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        // TODO MAKE THIS WORK
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateIssue(int id)
        {
            try
            {
                Request.Headers.TryGetValue("state", out var _state);
                // Check if valid state
                Enum.TryParse(_state.ToString().ToUpper(), out IssueState state);

                //Issue issue = new Issue { Id = id, IssueState = state.ToString() };
                Issue issue = await db.Issues.FirstOrDefaultAsync(p => p.Id == id);
                if (issue != null)
                {
                    issue.IssueState = state.ToString();
                    await db.SaveChangesAsync();
                    return Ok();
                }
                
                return NotFound();
            } catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}
