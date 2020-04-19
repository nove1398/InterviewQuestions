﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Interview.Api.Data;
using Interview.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Interview.gRPC.Services
{
    public class AgentsService : AgentManager.AgentManagerBase
    {
        private readonly ILogger<AgentsService> _ilog;
        private readonly DataContext _context;

        public AgentsService(ILogger<AgentsService> log, DataContext context)
        {
            _ilog = log;
            _context = context;
        }

        public override async Task<CreateAgentReply> Create(CreateAgentRequest request, ServerCallContext context)
        {
            //Create new resource
            CreateAgentReply reply = new CreateAgentReply();
            Agent tempAgent = new Agent();
            tempAgent.ContactNumber = request.ContactNumber;
            tempAgent.Name = request.Name.Trim();
            _context.Agents.Add(tempAgent);
            await _context.SaveChangesAsync();
            reply.Response = "Agent created";
            return await Task.FromResult(reply);
        }

        public override async Task<ReadAgentReply> ReadSingle(ReadAgentRequest request, ServerCallContext context)
        {
            var agent = await _context.Agents.AsNoTracking().FirstOrDefaultAsync(a => a.ContactNumber == request.ContactNumber);
            if(agent != null)
                return await Task.FromResult(new ReadAgentReply { Id = agent.AgentId, Name = agent.Name, ContactNumber = agent.ContactNumber });
            else
                return await Task.FromResult(new ReadAgentReply());
        }

        public override async Task ReadList(ReadAgentRequest request, IServerStreamWriter<ReadAgentReply> responseStream, ServerCallContext context)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                //Find by number
                var agents = await _context.Agents.AsNoTracking().Where(a => a.Name.Contains(request.Name)).ToListAsync();
                foreach(var agent in agents)
                {
                    await responseStream.WriteAsync(new ReadAgentReply { Id = agent.AgentId, Name = agent.Name, ContactNumber = agent.ContactNumber });
                }
            }
        }


        public override async Task<UpdateAgentReply> Update(UpdateAgentRequest request, ServerCallContext context)
        {
            //Find by id and update
            if (request.Id == 0)
            {
                return new UpdateAgentReply { Response = "No agents by that ID" };
            }

            //Update existing
            var tempAgent = _context.Agents.FirstOrDefault(a => a.AgentId == request.Id);
            if (tempAgent != null)
            {
                tempAgent.ContactNumber = request.ContactNumber;
                tempAgent.Name = request.Name.Trim();
                await _context.SaveChangesAsync();
                return new UpdateAgentReply { Response = "Agent updated" };
            }
            return new UpdateAgentReply { Response = "No agents by that ID" };
        }

        public override async Task<DeleteAgentReply> Delete(DeleteAgentRequest request, ServerCallContext context)
        {
            //Find by id and delete
            if (request.Id == 0)
            {
               return new DeleteAgentReply { Response = "No agents by that ID" };
            }

            var tempAgent = await _context.Agents.FirstOrDefaultAsync(a => a.AgentId == request.Id);
            if (tempAgent != null)
            {
                _context.Agents.Remove(tempAgent);
                await _context.SaveChangesAsync();
                return new DeleteAgentReply { Response = "Agent deleted" };
            }
            else
            {

                return new DeleteAgentReply { Response = "No agents by that ID" };
            }
        }
    }
}
