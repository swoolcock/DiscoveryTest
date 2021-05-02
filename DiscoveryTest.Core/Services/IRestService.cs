using System.Collections.Generic;
using System.Threading.Tasks;
using DiscoveryTest.Core.Model;

namespace DiscoveryTest.Core.Services
{
    /// <summary>
    /// Interface that allows mocking of the API.
    /// </summary>
    public interface IRestService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync(string parkCode, string arriving);
        Task<Dictionary<string, string>> PostResponseAsync(string resId, string userEmail);
    }
}