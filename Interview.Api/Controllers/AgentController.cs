using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Api.Data;
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
        private readonly DataContext _context;

        public AgentController(ILogger<AgentController> ilogger, DataContext context)
        {
            _ilog = ilogger;
            _context = context;
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok(new JsonResult(new { test = "val" }));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Agent newAgent)
        {
            if(newAgent != null)
            {
                Agent tempAgent = new Agent();
                tempAgent.ContactNumber = newAgent.ContactNumber;
                tempAgent.Name = newAgent.Name.Trim();
                _context.Agents.Add(tempAgent);
                _context.SaveChangesAsync();
                return CreatedAtAction("Create",new JsonResult(new { response = "Created agent" }));
            }
            else
            {

                return CreatedAtAction("Create",new JsonResult(new { response = "Invalid agent data" }));
            }
       
        }

        [HttpGet("{contact:int?}/{name}")]
        public IActionResult Read(int? contact = null, string name = null)
        {
            //Read either by number or name
            return Ok(new JsonResult(new { test = "R" }));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] Agent newAgent,int? id)
        {
            if(id == null)
            {
                return BadRequest(new JsonResult(new { response = "No agents by that ID" }));
            }

            //Update existing
            var tempAgent = _context.Agents.FirstOrDefault(a => a.AgentId == id);
            if(tempAgent != null)
            {
                tempAgent.ContactNumber = newAgent.ContactNumber;
                tempAgent.Name = newAgent.Name.Trim();
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest(new JsonResult(new { response = "No agents by that ID" }));
            }
            return Ok(new JsonResult(new { response = "Agent updated" }));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new JsonResult(new { response = "No agents by that ID" }));
            }

            var tempAgent = _context.Agents.FirstOrDefault(a => a.AgentId == id);
            if (tempAgent != null)
            {
                _context.Agents.Remove(tempAgent);
                await _context.SaveChangesAsync();
                return Ok(new JsonResult(new { test = "D" }));
            }
            else
            {

                return Ok(new JsonResult(new { response = "Invalid agent" }));
            }

        }
    }
}