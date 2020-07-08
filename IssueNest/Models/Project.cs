using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueNest.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int Owner_id { get; set; }
        public string Hook { get; set; }
    }
}
