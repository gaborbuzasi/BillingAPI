using Billing.Common;
using Microsoft.Extensions.Configuration;
using RequestDataCollector.SDK;
using System.IO;

namespace RequestDataCollector.Tests
{
    public class IntegrationTestBase
    {
        protected static RequestDataCollectorApi RequestDataCollectorApi { get; }

        static IntegrationTestBase()
        {
            var settings = File.ReadAllText("appsettings.json").Deserialize<RequestDataCollectorApiSettings>();

            // will require environment detection to run on CI buildserver or other dev machine
            var configuration = new ConfigurationBuilder().AddUserSecrets<IntegrationTestBase>().Build();

            settings.ApiKey = configuration["ApiKey"];

            RequestDataCollectorApi = new RequestDataCollectorApi(settings);
        }
    }
}
