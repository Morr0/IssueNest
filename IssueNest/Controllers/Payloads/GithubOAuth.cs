using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueNest.Controllers.Payloads
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubOAuth : ControllerBase
    {
        [Required]
        [NotNull]
        public string Code { get; set; }
    }
}
