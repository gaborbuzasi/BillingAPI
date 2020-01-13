using Billing.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Billing.API.SDK
{
    public class GetReportByMonthRequest : IApiRequest
    {
        public HttpMethod GetMethod() => HttpMethod.Get;

        public string GetUrl() => $"report/{SubscriptionId}/{Year}/{Month}";

        [FromRoute]
        public string SubscriptionId { get; set; }

        [FromRoute]
        public int Year { get; set; }
        [FromRoute]
        public int Month { get; set; }
    }
}
