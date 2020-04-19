using Grpc.Core;
using Grpc.Net.Client;
using Interview.gRPC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Interview.Client.Program;

namespace Interview.Client
{
    public class GrpcClient
    {

        public GrpcClient()
        {
            
        }

        public async Task LoadMenu()
        {
            Console.Clear();
            Console.WriteLine("-===++~gRPC Client~++===-");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1. Create \n2. Read \n3. Update \n4. Delete \n5. Main Menu");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int selectedOption))
            {
                switch (selectedOption)
                {
                    case 1:
                        await Create();
                        break;
                    case 2:
                        await Read();
                        break;
                    case 3:
                        await Update();
                        break;
                    case 4:
                        await Delete();
                        break;
                    case 5:
                        await init.LoadMenu();
                        break;
                    default:
                        await ErrorDisplay();
                        break;
                }

            }
            else
            {
                await ErrorDisplay();
            }
        }

        private async Task Delete(bool error = false)
        {
            if (error)
            {
                Error();
            }
                
            Console.WriteLine("    Delete    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Enter ID of agent to delete:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                var request = new DeleteAgentRequest { Id = id};
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new AgentManagerService.AgentManagerServiceClient(channel);
                var reply = await client.DeleteAsync(request);
                await PrintResults(reply.Response);
            }
            else
            {
                await Delete(true);
            }
        }

        private async Task Update(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Update    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Enter the agent ID# you wish to update:");
            var idNumber = Console.ReadLine().Trim();
            Console.WriteLine("Enter the new agent name:");
            var name = Console.ReadLine().Trim();
            Console.WriteLine("Enter the new agent contact number:");
            var contact = Console.ReadLine().Trim();

            //Make update web call
            if (int.TryParse(contact, out int contactNumber) && int.TryParse(idNumber, out int id))
            {
                var request = new AgentModel {Id = id, Name = name.Trim(),ContactNumber = contactNumber };
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new AgentManagerService.AgentManagerServiceClient(channel);
                var reply = await client.UpdateAsync(request);
                await PrintResults(reply.Response);
            }
        }

        private async Task Read(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Read    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Do you want to search by agent (1) name or (2) contact number?");
            var updateType = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(updateType, out int type))
            {
                switch (type)
                {
                    case 1:
                        //Search by name
                        Console.WriteLine("Enter the name to search for:");
                        var searchName = Console.ReadLine();
                        var request = new ReadAgentRequest { Name = searchName.Trim() };
                        var channel = GrpcChannel.ForAddress("https://localhost:5001");
                        var client = new AgentManagerService.AgentManagerServiceClient(channel);
                        using (var reply = client.ReadList(request))
                        {
                            if(reply != null)
                            {
                                while (await reply.ResponseStream.MoveNext())
                                {
                                    var current = reply.ResponseStream.Current;
                                    Console.WriteLine($"{current.Name} {current.ContactNumber}");
                                }
                            }   
                        }
                        break;
                    case 2:
                        //Search by contact
                        Console.WriteLine("Enter the contact number to search for:");
                        var contactNumber = Console.ReadLine();
                        if (int.TryParse(contactNumber, out int contact))
                        {
                            var request2 = new ReadAgentRequest { ContactNumber = contact };
                            var channel2 = GrpcChannel.ForAddress("https://localhost:5001");
                            var client2 = new AgentManagerService.AgentManagerServiceClient(channel2);
                            var reply = client2.ReadSingle(request2);
                            if(reply != null)
                                await PrintResults($"{reply.Id} {reply.Name} {reply.ContactNumber}");
                        }
                        break;
                }
                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadLine();
                await LoadMenu();
            }
        }

        private async Task Create(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Create    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Enter the agent name:");
            var name = Console.ReadLine().Trim();
            Console.WriteLine("Enter the agent contact number:");
            var contact = Console.ReadLine().Trim();

            //Make web call
            if (int.TryParse(contact, out int contactNumber))
            {
                var request = new AgentModel { Name = name, ContactNumber = contactNumber };
                var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new AgentManagerService.AgentManagerServiceClient(channel);
                var result = await client.CreateAsync(request);
                await PrintResults(result.Response);
            }
        }

        private async Task PrintResults(string input)
        {
            Console.WriteLine($"{input}");
            Console.WriteLine("");
            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadLine();
            await LoadMenu();
        }

        private void Error()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("");
        }

        private async Task ErrorDisplay()
        {
            Console.WriteLine("Invalid input");
            await LoadMenu();
        }
    }
}
