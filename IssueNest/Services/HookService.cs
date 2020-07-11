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
        IssuesDBContext db;
        public HookService(IssuesDBContext db)
        {
            this.db = db;
        }

        public HookIssue HandleGithub(JsonElement payload)
        {
            HookIssue hookIssue = new HookIssue { };

            if (payload.ValueKind == JsonValueKind.Object)
            {
                // Base values
                bool existingIssue = false; // true -> the issue is already there so we update it
                IssueState? newState = null;

                foreach (JsonProperty prop in payload.EnumerateObject())
                {
                    if (prop.Name == "action")
                    {
                        // Value of action
                        // Will not consider all values
                        // From Github docs https://developer.github.com/webhooks/event-payloads/#issues
                        switch (prop.Value.GetString())
                        {
                            case "opened":
                            case "reopened":
                            case "unlocked":
                                newState = IssueState.EXISTING;
                                break;
                            case "deleted":
                                newState = IssueState.DELETED;
                                break;
                            case "closed": 
                            case "locked":
                            case "transferred":
                                newState = IssueState.CLOSED;
                                break;
                        }
                    }


                }
            }

            return hookIssue;
        }
    }
}
