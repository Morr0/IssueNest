using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IssueNest.Models
{
    public class Project
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        [NotNull]
        public int UserId { get; set; }
        public User User { get; set; }
        [NotNull]
        [Required]
        public string Name { get; set; }

        public string RepositoryUrl { get; set; }

        [NotNull]
        public string Hook { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class ProjectWriteDTO
    {
        [ForeignKey("User")]
        [NotNull]
        [Required]
        public int UserId { get; set; }
        [NotNull]
        [Required]
        public string Name { get; set; }
        public string RepositoryUrl { get; set; }
    }

    public class ProjectReadDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string RepositoryUrl { get; set; }
        public string Hook { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
