using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using IssueNest.Data;
using IssueNest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IssueNest.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private IssuesDBContext db;
        private IMapper _mapper;

        public ProjectController(IssuesDBContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectWriteDTO _project)
        {
            Project project = _mapper.Map<Project>(_project);

            if (await db.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == project.UserId) != null)
            {
                project.Hook = Guid.NewGuid().ToString();

                await db.Projects.AddAsync(project);
                await db.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }  

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            Project project = await db.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return project == null ? (IActionResult) NotFound() : Ok(_mapper.Map<ProjectReadDTO>(project));
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            if (await db.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == userId) == null)
                return NotFound();

            List<Project> list = await db.Projects.AsNoTracking().Where(p => p.UserId == userId)
                .ToListAsync();
            return Ok(_mapper.Map<List<ProjectReadDTO>>(list));
        }
    }
}
