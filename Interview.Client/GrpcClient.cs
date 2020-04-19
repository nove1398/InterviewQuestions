using Grpc.Net.Client;
using Interview.gRPC;
using System;
using System.Collections.Generic;
using System.Text;
using static Interview.Client.Program;

namespace Interview.Client
{
    public class GrpcClient
    {

        public GrpcClient()
        {
            
        }

        public void LoadMenu()
        {
            Console.Clear();
            Console.WriteLine("-===++~gRPC Client~++===-");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1. Create 2. Read 3. Update 4. Delete 5. Main Menu");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int selectedOption))
            {
                switch (selectedOption)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Read();
                        break;
                    case 3:
                        Update();
                        break;
                    case 4:
                        Delete();
                        break;
                    case 5:
                        init.LoadMenu();
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

        private void Delete(bool error = false)
        {
            Console.Clear();
            if (error)
            {
                Error();
            }
                
            Console.WriteLine("    Delete    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Enter id of agent to delete:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int id))
            {
                Console.WriteLine($"{id} deleted");
            }
            else
            {
                Delete(true);
            }
        }

        private void Update(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Update    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
        }

        private void Read(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Read    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
        }

        private void Create(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("    Create    ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
        }

        private void Error()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("");
        }

        private void ErrorDisplay()
        {
            Console.WriteLine("Invalid input");
            LoadMenu();
        }
    }
}
