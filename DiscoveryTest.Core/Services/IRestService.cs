using System.Collections.Generic;
using System.Threading.Tasks;
using DiscoveryTest.Core.Model;

namespace DiscoveryTest.Core.Services
{
    public interface IRestService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync(string parkCode, string arriving);
        Task<Dictionary<string, string>> PostResponseAsync(string resId, string userEmail);
    }
}