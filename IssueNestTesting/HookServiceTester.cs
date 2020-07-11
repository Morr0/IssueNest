using IssueNest.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;
using Xunit;

namespace IssueNestTesting
{
    public class HookServiceTester
    {
        IHookService service;
        public HookServiceTester()
        {
            this.service = new HookService();
        }

        // From https://developer.github.com/webhooks/event-payloads/#issues
        [Fact]
        public async void TestGituhbPayload()
        {
            string text = await File.ReadAllTextAsync("../../../Jsons/hook_issue_github_payload.json");
            JsonDocument doc = JsonDocument.Parse(text);

            HookIssue hookIssue = service.HandleGithub(doc.RootElement);
            Console.WriteLine(hookIssue);

            Assert.True(hookIssue.Issue.Title == "Spelling error in the README file", "Title is wrong");
            Assert.True(hookIssue.Existing, "The issue already does not exist");
        }
    }
}
