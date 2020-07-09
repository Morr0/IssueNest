using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Models
{
    public class Issue
    {
        public int Id { get; set; }
        
        [ForeignKey("Project")]
        [NotNull]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [NotNull]
        public string Description { get; set; }

        [NotNull]
        public string IssueFrom { get; set; }
        [NotNull]
        public string IssueType { get; set; }
        [NotNull]
        public string IssueState { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
