namespace RequestDataCollector.SDK.Models
{
    public class Request
    {
        public string Id { get; set; }
        public string Target { get; set; }
        public string Requested { get; set; }
        public string OriginIp { get; set; }
        public string Duration { get; set; }
        public string ServiceName { get; set; }
        public string SubscriptionId { get; set; }

    }
}
