using Billing.API.SDK;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Billing.API.Tests
{
    public class IntegrationTestsBase
    {
        protected static TestServer ApiServer;
        protected static BillingApi Api;

        static IntegrationTestsBase()
        {
            var apiSettingsPath = "appsettings.json";

            var apiConfiguration = new ConfigurationBuilder()
                                        .AddJsonFile(apiSettingsPath)
                                        .AddUserSecrets<Startup>()
                                        .Build();

            var settings = apiConfiguration.Get<BillingApiSettings>();

            var apiBuilder = new WebHostBuilder().UseConfiguration(apiConfiguration).UseStartup<Startup>();

            
            ApiServer = new TestServer(apiBuilder);

            Api = new BillingApi(settings, ApiServer.CreateClient());
        }
    }
}
