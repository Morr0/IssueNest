using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Controllers.Payloads
{
    public class UserAuthLoginPayload
    {
        [Required]
        [NotNull]
        public string Email { get; set; }
        [Required]
        [NotNull]
        public string Password { get; set; }
    }
}
