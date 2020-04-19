using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Interview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private ILogger<AgentController> _ilog;

        public AgentController(ILogger<AgentController> ilogger)
        {
            _ilog = ilogger;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new JsonResult(new { test = "val" }));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Agent newAgent)
        {
            return CreatedAtAction("Create",new JsonResult(new { test = newAgent.Name }));
        }

        [HttpGet("{id:int}")]
        public IActionResult Read()
        {
            return Ok(new JsonResult(new { test = "R" }));
        }

        [HttpPut("{id:int}")]
        public IActionResult Update()
        {
            return Ok(new JsonResult(new { test = "U" }));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete()
        {
            return Ok(new JsonResult(new { test = "D" }));
        }
    }
}