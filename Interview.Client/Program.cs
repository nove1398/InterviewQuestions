using Grpc.Core;
using Grpc.Net.Client;
using Interview.gRPC;
using System;
using System.Threading.Tasks;

namespace Interview.Client
{
    class Program
    {
        public static InterviewMain init;
        private static string UserName = "";

        static async Task Main(string[] args)
        {
            var request = new CreateAgentRequest { Name = "joe",ContactNumber = 1234567891 };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new AgentManager.AgentManagerClient(channel);
            using (var reply = client.Read(new ReadAgentRequest()))
            {
                while (await reply.ResponseStream.MoveNext())
                {
                    var current = reply.ResponseStream.Current;
                    Console.WriteLine($"{current.Response}");

                }
            }
            Console.WriteLine("What would you like to be called?");
            UserName = Console.ReadLine()?.Trim() ?? "Unknown soldier" ;

            init = new InterviewMain();
            init.LoadMenu();
        }

        public class InterviewMain
        {
            private GrpcClient gRpc;
            private RestClient restClient;

            public InterviewMain()
            {
                gRpc = new GrpcClient();
                restClient = new RestClient();
            }

            public void LoadMenu()
            {
                Console.Clear();
                Console.WriteLine("++++++++++ Main Menu ++++++++++");
                Console.WriteLine("What would you like to work with today?");
                Console.WriteLine("1. gRPC Client");
                Console.WriteLine("2. REST Client");
                Console.WriteLine("3. Exit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case 1:
                            GrpcMenu();
                            break;
                        case 2:
                            RestMenu();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            ErrorDisplay();
                            break;
                    }

                }
                else
                {
                    ErrorDisplay();
                }
            }

            private void ErrorDisplay()
            {
                Console.WriteLine("Invalid selection");
                LoadMenu();
            }

            private void GrpcMenu()
            {
                //Call grpc class and anything else
                gRpc.LoadMenu();
               
            }

            private void RestMenu()
            {
                //Call rest class and anything else
                restClient.LoadMenu();
            }
        }

        
    }
}
