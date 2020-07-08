using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public string Issue_from { get; set; }
        public string Issue_type { get; set; }
        public string Issue_state { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
