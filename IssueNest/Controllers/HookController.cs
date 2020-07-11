using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using IssueNest.Data;
using IssueNest.Services;
using Microsoft.AspNetCore.Mvc;

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
            hookService.HandleGithub(json);
            if (db.Projects.Where(p => p.Hook == hook).Any())
            {
                // DO the stuff
                
            }

            return NotFound();
        }
    }
}
