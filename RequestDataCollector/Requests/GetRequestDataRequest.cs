using Billing.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace RequestDataCollector.SDK
{
    public class GetRequestDataRequest : IApiRequest
    {
        public HttpMethod GetMethod() => HttpMethod.Get;

        public string GetUrl() => $"requests/{Year}/{Month}";

        [FromRoute]
        public int Year { get; set; }
        [FromRoute]
        public int Month { get; set; }
    }
}
