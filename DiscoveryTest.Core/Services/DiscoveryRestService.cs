using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiscoveryTest.Core.Model;
using Newtonsoft.Json;

namespace DiscoveryTest.Core.Services
{
    public class DiscoveryRestService : IRestService
    {
        private const string api_host = "discoverycodetest.azurewebsites.net";
        private string getCustomersEndpoint(string parkCode, string arriving) => $"https://{api_host}/api/NPS/Customers?parkCode={parkCode}&arriving={arriving}";
        private string postResponseEndpoint() => $"https://{api_host}/api/NPS/Response";

        private readonly HttpClient client = new HttpClient();

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync(string parkCode, string arriving)
        {
            var uri = new Uri(getCustomersEndpoint(parkCode, arriving));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CustomerDTO>>(content);
            }
            
            // TODO: throw an exception
            return Enumerable.Empty<CustomerDTO>();
        }

        public async Task<Dictionary<string, string>> PostResponseAsync(string resId, string userEmail)
        {
            var uri = new Uri(postResponseEndpoint());
            var body = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {"ResId", resId},
                {"UserEmail", userEmail}
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return null;
            
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
        }
    }
}
