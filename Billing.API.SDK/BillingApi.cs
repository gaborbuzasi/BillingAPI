using Billing.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Billing.API.SDK
{
    public class BillingApi : IBillingApi
    {
        BillingApiSettings Settings { get; }
        Lazy<HttpClient> Client { get; }

        public BillingApi(BillingApiSettings settings, HttpClient client = null)
        {
            Settings = settings;

            if (client == null)
            {
                Client = new Lazy<HttpClient>(InitializeClient);
            }
            else
            {
                Client = new Lazy<HttpClient>(() => client);
            }
        }

        public Task<GetReportByMonthResponse> GetReportByMonth(GetReportByMonthRequest request)
        {
            return SendAsync<GetReportByMonthResponse>(request);
        }

        #region generic api methods
        // These methods could be exported into a common API object and this API class inheriting it

        private async Task<T> SendAsync<T>(IApiRequest request) where T : class
        {
            var url = $"{Settings.Url}/{request.GetUrl()}";

            var message = new HttpRequestMessage
            {
                Method = request.GetMethod(),
                RequestUri = new Uri(url)
            };

            var response = await Client.Value.SendAsync(message);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"{response.StatusCode} - {responseContent}");
            }

            return responseContent.Deserialize<T>();
        }

        private HttpClient InitializeClient()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        #endregion
    }
}
