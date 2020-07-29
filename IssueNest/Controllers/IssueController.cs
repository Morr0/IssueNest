using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _mapper;

        public IssueController(IssuesDBContext db, IMapper mapper)
        {
            this.db = db;
            //this.manager = manager;
            _mapper = mapper;
        }

        [HttpGet("{projectId}/{issueId}")]
        public async Task<IActionResult> GetIssue(int projectId, int issueId)
        {
            Issue issue = await db.Issues.FirstOrDefaultAsync(p => p.ProjectId == projectId && p.Id == issueId);
            if (issue != null)
            {
                return Ok(_mapper.Map<IssueReadDTO>(issue));
            }

            return NotFound();
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetIssues(int projectId)
        {
            if (await db.Projects.FindAsync(projectId) == null)
                return NotFound();

            List<Issue> list = await db.Issues.Where(p => p.ProjectId == projectId)
                .ToListAsync();
            return Ok(_mapper.Map<List<IssueReadDTO>>(list));
        }
    }
}
