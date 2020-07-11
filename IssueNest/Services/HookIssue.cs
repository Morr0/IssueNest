using IssueNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public struct HookIssue
    {
        Issue issue;

        // IF THE ISSUE EXISTED BEFORE
        bool existing;
        int existingIssueId;
    }
}
