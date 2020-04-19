using System;

namespace Interview.Client
{
    class Program
    {
        public static InterviewMain init;

        static void Main(string[] args)
        {

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
                Console.WriteLine("Choose your client type");
                Console.WriteLine("1. gRPC calls");
                Console.WriteLine("2. REST calls");
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
