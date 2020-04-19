using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interview.Api.Data;
using Interview.Shared.Models;
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
                return new JsonResult(new ApiResponse { Response = "Created agent" });
            }
            else
            {

                return new JsonResult(new ApiResponse { Response = "Invalid agent data" });
            }
       
        }

        [HttpGet]
        public async Task<IActionResult> Read(int? contact = null, string name = null)
        {
            //Read either by number or name
            if (!string.IsNullOrEmpty(name))
            {
                var agents = await _context.Agents.AsNoTracking().Where(a => a.Name.Contains(name)).ToListAsync();
                return new JsonResult(new ApiResponse { Response = "Agents found", DataList = agents });
            }
            else if(contact.HasValue)
            {
                var agent = await _context.Agents.AsNoTracking().FirstOrDefaultAsync(a => a.ContactNumber == contact);
                return new JsonResult(new ApiResponse { Response = "Agent found", Data = agent });
            }
            else
            {
                 return new JsonResult(new ApiResponse { Response = "Invalid search criteria" });

            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Agent newAgent,int? id = null)
        {
            if(id == null)
            {
                return new JsonResult(new ApiResponse { Response = "No agents by that ID" });
            }

            //Update existing
            var tempAgent = _context.Agents.FirstOrDefault(a => a.AgentId == id);
            if(tempAgent != null)
            {
                tempAgent.ContactNumber = newAgent.ContactNumber;
                tempAgent.Name = newAgent.Name.Trim();
                await _context.SaveChangesAsync();
                return new JsonResult(new ApiResponse{ Response = "Agent updated" });
            }
                return new JsonResult(new ApiResponse{ Response = "No agents by that ID" });
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id = null)
        {
            if (id == null)
            {
                return new JsonResult(new { response = "No agents by that ID" });
            }

            var tempAgent = await  _context.Agents.FirstOrDefaultAsync(a => a.AgentId == id);
            if (tempAgent != null)
            {
                _context.Agents.Remove(tempAgent);
                await _context.SaveChangesAsync();
                return new JsonResult(new ApiResponse{ Response = "Agent deleted" });
            }
            else
            {

                return new JsonResult(new ApiResponse { Response = "Invalid agent ID" });
            }

        }
    }
}