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


        static async Task Main(string[] args)
        {
            Console.WriteLine("What would you like to be called?");
            string userName = Console.ReadLine()?.Trim() ?? "Unknown soldier" ;

            init = new InterviewMain(userName);
            await init.LoadMenu();
        }

        public class InterviewMain
        {
            private GrpcClient Grpc;
            private RestClient RestClient;
            private string UserName = "";

            public InterviewMain(string name)
            {
                UserName = name;
                Grpc = new GrpcClient();
                RestClient = new RestClient();
            }

            public async Task LoadMenu()
            {
                Console.Clear();
                Console.WriteLine("++++++++++ Main Menu ++++++++++");
                Console.WriteLine($"What would you like to work with today {UserName}?");
                Console.WriteLine("1. gRPC Client");
                Console.WriteLine("2. REST Client");
                Console.WriteLine("3. Exit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int selectedOption))
                {
                    switch (selectedOption)
                    {
                        case 1:
                            await GrpcMenu();
                            break;
                        case 2:
                            await RestMenu();
                            break;
                        case 3:
                            Environment.Exit(0);
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

            private async Task ErrorDisplay()
            {
                Console.WriteLine("Invalid selection");
                await LoadMenu();
            }

            private async Task GrpcMenu()
            {
                //Call grpc class and anything else
                await Grpc.LoadMenu();
               
            }

            private async Task RestMenu()
            {
                //Call rest class and anything else
                await RestClient.LoadMenu();
            }
        }

        
    }
}
