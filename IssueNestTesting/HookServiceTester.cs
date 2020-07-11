using IssueNest.Services;
using System;
using Xunit;

namespace IssueNestTesting
{
    public class HookServiceTester
    {
        IHookService service;
        public HookServiceTester()
        {
            //this.service = new HookService()
        }
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}
