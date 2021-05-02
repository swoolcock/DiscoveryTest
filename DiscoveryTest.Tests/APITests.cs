using System.Threading.Tasks;
using DiscoveryTest.Core.Model;
using DiscoveryTest.Core.Services;
using NUnit.Framework;

namespace DiscoveryTest.Tests
{
    public class APITests
    {
        private IRestService restService;
        
        [SetUp]
        public void Setup()
        {
            // TODO: use a proper mocked API
            restService = new DiscoveryRestService();
        }

        [Test]
        public async Task TestGetCustomers()
        {
            var results = await restService.GetCustomersAsync("WROT", "2020-12-02");
            Assert.AreEqual(results, new[]
            {
                new CustomerDTO
                {
                    ReservationId = "11614-31188",
                    GuestName = "MR BOB SMITH",
                    GuestMobile = "0419 000 111",
                    Arrived = "2020-12-02",
                    Depart = "2020-12-03",
                    Category = "X. Other",
                    Nights = "1",
                    AreaName = "WROT-EW023",
                    PreviousNPS = null,
                    PreviousNPSComment = "",
                }
            });
        }
    }
}
