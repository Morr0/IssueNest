using IssueNest.Models;
using IssueNest.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public interface IHookService
    {
        bool VerifyGithubHeaders(IHeaderDictionary headers);
        HookIssue HandleGithubPayload(JsonElement payload);
    }
}
