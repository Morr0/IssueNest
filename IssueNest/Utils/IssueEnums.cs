using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Utils
{
    public enum IssueFrom
    {
        GITHUB
    }

    public enum IssueType
    {
        MINOR,
        SEVERE,
        FATAL
    }

    public enum IssueState
    {
        NEW,
        PROGRESS,
        FIXED,
        IGNORED
    }
}
