using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Api.Data;
using Interview.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create([FromBody] Agent newAgent)
        {
            if(newAgent != null)
            {
                Agent tempAgent = new Agent();
                tempAgent.ContactNumber = newAgent.ContactNumber;
                tempAgent.Name = newAgent.Name.Trim();
                _context.Agents.Add(tempAgent);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Create",new JsonResult(new { response = "Created agent" }));
            }
            else
            {

                return BadRequest(new JsonResult(new { response = "Invalid agent data" }));
            }
       
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? contact = null, string name = null)
        {
            //Read either by number or name
            if (!string.IsNullOrEmpty(name))
            {
                var agents = await _context.Agents.AsNoTracking().Where(a => a.Name.Contains(name)).ToListAsync();
                return Ok(new JsonResult(new { response= "Agents found", data = agents }));
            }
            else if(contact.HasValue)
            {
                var agent = await _context.Agents.AsNoTracking().FirstOrDefaultAsync(a => a.ContactNumber == contact);
                return Ok(new JsonResult(new { response ="Agent found", data = agent }));
            }
            else
            {
                 return NotFound(new JsonResult(new { response = "Invalid search criteria" }));

            }
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
                return Ok(new JsonResult(new { response = "Agent updated" }));
            }
                return NotFound(new JsonResult(new { response = "No agents by that ID" }));
            
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new JsonResult(new { response = "No agents by that ID" }));
            }

            var tempAgent = await  _context.Agents.FirstOrDefaultAsync(a => a.AgentId == id);
            if (tempAgent != null)
            {
                _context.Agents.Remove(tempAgent);
                await _context.SaveChangesAsync();
                return Ok(new JsonResult(new { response = "Agent deleted" }));
            }
            else
            {

                return NotFound(new JsonResult(new { response = "Invalid agent ID" }));
            }

        }
    }
}