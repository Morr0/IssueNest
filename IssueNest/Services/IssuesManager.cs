using IssueNest.Data;
using IssueNest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public class IssuesManager
    {
        private readonly IssuesDBContext db;
        public IssuesManager(IssuesDBContext db)
        {
            this.db = db;
        }

        //public async Task<Issue> CreateIssue(hook)
        //{
        //    db.Projects.Add()
        //}
    }
}
