using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Shared.Models
{
    public class Agent
    {
        [Key]
        public int AgentId { get; set; }

        public string Name { get; set; }

        public int ContactNumber { get; set; }
    }
}
