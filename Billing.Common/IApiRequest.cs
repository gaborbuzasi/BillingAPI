using System.Net.Http;

namespace Billing.Common
{
    public interface IApiRequest
    {
        HttpMethod GetMethod();
        string GetUrl();
    }
}
