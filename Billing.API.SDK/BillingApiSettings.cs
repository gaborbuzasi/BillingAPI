using RequestDataCollector.SDK;

namespace Billing.API.SDK
{
    public class BillingApiSettings
    {
        public RequestDataCollectorApiSettings RequestDataCollector { get; set; }
        public string Url { get; set; }
    }
}
