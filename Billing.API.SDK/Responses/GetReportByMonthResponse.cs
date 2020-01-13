using System;

namespace Billing.API.SDK
{
    public class GetReportByMonthResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string SubscriptionId { get; set; }
        public int TotalNumberOfRequests { get; set; }
        public string ServiceType { get; set; }
        public Money Cost { get; set; }
    }
}
