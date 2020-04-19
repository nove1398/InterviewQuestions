using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Interview.gRPC.Services
{
    public class AgentsService : AgentManager.AgentManagerBase
    {
        private readonly ILogger<AgentsService> _ilog;

        public AgentsService(ILogger<AgentsService> log)
        {
            _ilog = log;
        }

        public override Task<CreateAgentReply> Create(CreateAgentRequest request, ServerCallContext context)
        {
            //Create new resource
            CreateAgentReply reply = new CreateAgentReply();
            reply.Response = $"{new Random().Next(10, 1000)} | {request.Name} | {request.ContactNumber} ";
            return Task.FromResult(reply);
        }

        public override Task Read(ReadAgentRequest request, IServerStreamWriter<ReadAgentReply> responseStream, ServerCallContext context)
        {


            if (request.ContactNumber == 0)
            {
                //Find by name
            }
            else if (string.IsNullOrEmpty(request.Name))
            {
                //Find by number
            }
            else
            {
                //Find by both
            }
            return null;
        }


        public override Task<UpdateAgentReply> Update(UpdateAgentRequest request, ServerCallContext context)
        {
            //Find by id and update
            return base.Update(request, context);
        }

        public override Task<DeleteAgentReply> Delete(DeleteAgentRequest request, ServerCallContext context)
        {
            //Find by id and delete
            return base.Delete(request, context);
        }
    }
}
