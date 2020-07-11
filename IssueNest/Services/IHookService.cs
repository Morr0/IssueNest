using IssueNest.Models;
using IssueNest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public interface IHookService
    {
        HookIssue HandleGithub(JsonElement body);
    }
}
