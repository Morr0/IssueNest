using IssueNest.Data;
using IssueNest.Models;
using IssueNest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IssueNest.Services
{
    public class HookService : IHookService
    {
        public HookIssue HandleGithub(JsonElement payload)
        {
            HookIssue hookIssue = new HookIssue
            {
                IssueFrom = IssueFrom.GITHUB,
                Issue = new Issue(),
                IssueType = IssueType.MINOR,
            };

            if (payload.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty prop in payload.EnumerateObject())
                {
                    // Issue's current action
                    if (prop.Name == "action")
                    {
                        // Value of action
                        // Will not consider all values
                        // From Github docs https://developer.github.com/webhooks/event-payloads/#issues
                        switch (prop.Value.GetString())
                        {
                            case "reopened":
                            case "unlocked":
                                hookIssue.IssueState = IssueState.EXISTING;
                                break;
                            case "opened":
                                hookIssue.Existing = false;
                                hookIssue.IssueState = IssueState.EXISTING;
                                break;
                            case "deleted":
                                hookIssue.IssueState = IssueState.DELETED;
                                break;
                            case "closed": 
                            case "locked":
                            case "transferred":
                                hookIssue.IssueState = IssueState.CLOSED;
                                break;
                        }
                    }

                    // Issue object
                    if (prop.Name == "issue")
                    {
                        foreach (JsonProperty issueProp in prop.Value.EnumerateObject())
                        {
                            // URL of the issue
                            if (issueProp.Name == "url")
                            {
                                hookIssue.Issue.IssueUrl = issueProp.Value.GetString();
                            }

                            // Repository URL
                            if (issueProp.Name == "repository_url")
                            {
                                hookIssue.Issue.RepositoryUrl = issueProp.Value.GetString();
                            }

                            // Title
                            if (issueProp.Name == "title")
                            {
                                hookIssue.Issue.Title = issueProp.Value.GetString();
                            }

                            // Description
                            if (issueProp.Name == "body")
                            {
                                hookIssue.Issue.Description = issueProp.Value.GetString();
                            }
                        }
                    }
                }
            }

            return hookIssue;
        }
    }
}
