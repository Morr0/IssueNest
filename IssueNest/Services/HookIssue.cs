using IssueNest.Models;
using IssueNest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public class HookIssue
    {
        public Issue Issue;

        public IssueState IssueState;
        public IssueType IssueType;
        public IssueFrom IssueFrom;

        // IF THE ISSUE EXISTED BEFORE
        public bool Existing = true;
    }
}
