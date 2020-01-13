using System.Threading.Tasks;

namespace Billing.API.SDK
{
    public interface IBillingApi
    {
        Task<GetReportByMonthResponse> GetReportByMonth(GetReportByMonthRequest request);
    }
}
