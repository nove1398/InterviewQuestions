using Interview.Api.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Interview.Client.Program;

namespace Interview.Client
{
    public class RestClient
    {
        private static readonly HttpClient _http = new HttpClient();
    

        public RestClient()
        {

            _http.BaseAddress = new Uri("Https://localhost:44318/api/agent");
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task LoadMenu()
        {
            Console.Clear();
            Console.WriteLine("-===++~ REST Client ~++===-");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1. Create 2. Read 3. Update 4. Delete 5. Main Menu");
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
                        ErrorDisplay();
                        break;
                }

            }
            else
            {
                ErrorDisplay();
            }
        }

        private async Task Delete(bool error = false)
        {
            Console.Clear();
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
                var respMessage = await _http.DeleteAsync($"?id={id}").ConfigureAwait(false);
                var response = await respMessage.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<ApiResponse>(response);
                await PrintResponse(results.Response);
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
                var json = JsonConvert.SerializeObject(new Agent { Name = name, ContactNumber = contactNumber });
                var payLoad = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await _http.PutAsync($"?id={id}", payLoad).ConfigureAwait(false);
                var response = await httpResponse.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<ApiResponse>(response);
                await PrintResponse(results.Response);
            }
        }

        private async Task Read(bool error = false)
        {
            if (error)
            {
                Error();
            }
            Console.WriteLine("     Read     ");
            Console.WriteLine("++++++++++++++");
            Console.WriteLine("");
            Console.WriteLine("Do you want to search by agent (1) name or (2) contact number?");
            var updateType = Console.ReadLine();
            Console.WriteLine();
            
            if(int.TryParse(updateType,out int type))
            {
                switch (type)
                {
                    case 1:
                        //Search by name
                        Console.WriteLine("Enter the name to search for:");
                        var searchName = Console.ReadLine();
                        var httpresponse = await _http.GetAsync($"?name={searchName}").ConfigureAwait(false);
                        var response = await httpresponse.Content.ReadAsStringAsync();
                        var results = JsonConvert.DeserializeObject<ApiResponse>(response);
                        foreach (var item in results.DataList)
                        {
                            Console.WriteLine($"{item.AgentId} | {item.Name}  | {item.ContactNumber}");
                            Console.WriteLine("Press 'Enter' to continue...");
                            Console.ReadLine();
                            await LoadMenu();
                        }
                        break;
                    case 2:
                        //Search by contact
                        Console.WriteLine("Enter the contact number to search for:");
                        var searchContact = Console.ReadLine();
                        var httpResponse2 = await _http.GetAsync($"?contact={searchContact}");
                        var response2 = await httpResponse2.Content.ReadAsStringAsync();
                        var api = JsonConvert.DeserializeObject<ApiResponse>(response2);
                        Console.WriteLine($"{api.Data.AgentId} | {api.Data.Name}  | {api.Data.ContactNumber}");
                        Console.WriteLine("Press 'Enter' to continue...");
                        Console.ReadLine();
                        await LoadMenu();
                        break;
                }
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
            if(int.TryParse(contact, out int contactNumber))
            {
                var json = JsonConvert.SerializeObject(new Agent { Name = name, ContactNumber = contactNumber });
                var payLoad = new StringContent(json, Encoding.UTF8, "application/json");
                var httpResponse = await _http.PostAsync("", payLoad).ConfigureAwait(false);
                var response = await httpResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ApiResponse>(response);
                await PrintResponse(data.Response);
            }
            
        }

        private async Task PrintResponse(string input)
        {
            Console.WriteLine($"{input}");
            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadLine();
            await LoadMenu();
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
