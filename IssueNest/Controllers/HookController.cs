using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using IssueNest.Data;
using IssueNest.Models;
using IssueNest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IssueNest.Controllers
{
    [Route("api/hook")]
    [ApiController]
    public class HookController : ControllerBase
    {
        private IssuesDBContext db;
        IHookService hookService;

        public HookController(IssuesDBContext db, IHookService hookService)
        {
            this.db = db;
            this.hookService = hookService;
            //this.manager = manager;
        }
        [HttpPost("{hook}")]
        public async Task<IActionResult> OnHook(string hook, [FromBody] JsonElement json)
        {
            if (db.Projects.Where(p => p.Hook == hook).Any())
            {
                if (hookService.VerifyGithubHeaders(Request.Headers))
                {
                    HookIssue hookIssue = hookService.HandleGithubPayload(json);

                    // Mapping values
                    hookIssue.Issue.IssueFrom = hookIssue.IssueFrom.ToString();
                    hookIssue.Issue.IssueState = hookIssue.IssueState.ToString();
                    hookIssue.Issue.IssueType = hookIssue.IssueType.ToString();

                    if (hookIssue.Existing) // Prexisting issue to be updated
                    {
                        Issue toBeUpdatedIssue = await db.Issues.FirstOrDefaultAsync(p => p.IssueUrl == hookIssue.Issue.IssueUrl);
                        if (toBeUpdatedIssue != null)
                        {
                            toBeUpdatedIssue.IssueFrom = hookIssue.Issue.IssueFrom;
                            toBeUpdatedIssue.IssueState = hookIssue.Issue.IssueState;
                            toBeUpdatedIssue.IssueType = hookIssue.Issue.IssueType;
                            toBeUpdatedIssue.Title = hookIssue.Issue.Title;
                            toBeUpdatedIssue.Description = hookIssue.Issue.Description;
                            toBeUpdatedIssue.RepositoryUrl = hookIssue.Issue.RepositoryUrl;
                            toBeUpdatedIssue.IssueUrl = hookIssue.Issue.IssueUrl;

                            await db.SaveChangesAsync();
                        }

                        return BadRequest();
                    } else // A new issue
                    {
                       db.Issues.Add(hookIssue.Issue);
                    }

                    return Ok();
                }

                return BadRequest();          
            }

            return NotFound();
        }
    }
}
