using RequestDataCollector.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace RequestDataCollector.Tests
{
    [TestClass]
    public class GetRequestDataTests : IntegrationTestBase
    {
        [TestMethod]
        public async Task GetRequestData_Success()
        {
            var request = new GetRequestDataRequest
            {
                Month = 3,
                Year = 2019
            };

            var result = await RequestDataCollectorApi.GetRequestData(request);

            Assert.IsNotNull(result);
        }
    }
}
