using Interview.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interview.Shared.Models
{
    public class ApiResponse
    {
        public string Response { get; set; }

        public Agent Data { get; set; }

        public List<Agent> DataList { get; set; }
    }
}
