using Billing.Common;
using RequestDataCollector.SDK.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestDataCollector.SDK
{
    public class RequestDataCollectorApi : IRequestDataCollectorApi
    {
        RequestDataCollectorApiSettings Settings { get; }
        Lazy<HttpClient> Client { get; }

        public RequestDataCollectorApi(RequestDataCollectorApiSettings settings)
        {
            Settings = settings;
            Client = new Lazy<HttpClient>(InitializeClient);
        }

        public Task<GetRequestDataResponse> GetRequestData(GetRequestDataRequest request)
        {
            return SendAsync<GetRequestDataResponse>(request);
        }

        #region generic api methods
        // These methods could be exported into a common API object and this API class inheriting it

        private async Task<T> SendAsync<T>(IApiRequest request) where T : class
        {
            var url = $"{Settings.Url}/{request.GetUrl()}?code={Settings.ApiKey}";

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
