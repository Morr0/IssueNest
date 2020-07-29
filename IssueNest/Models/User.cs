
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Models
{
    public class User
    {
        public int Id { get; set; }

        [NotNull]
        [Required]
        public string Name { get; set; }
        [NotNull]
        [Required]
        public string Email { get; set; }
        [NotNull]
        [Required]
        public string Password { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class UserWriteDto
    {
        [NotNull]
        [Required]
        public string Name { get; set; }
        [NotNull]
        [Required]
        public string Email { get; set; }
        [NotNull]
        [Required]
        public string Password { get; set; }
    }
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
