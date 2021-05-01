using System.Collections.Generic;
using System.Threading.Tasks;
using DiscoveryTest.Core.Model;

namespace DiscoveryTest.Core.Services
{
    public interface IRestService
    {
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync(string parkCode, string arriving);
        Task PostResponseAsync(string resId, string userEmail);
    }
}