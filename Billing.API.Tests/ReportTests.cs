using Billing.API.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Billing.API.Tests
{
    [TestClass]
    public class ReportTests : IntegrationTestsBase
    {

        [DataTestMethod]
        [DataRow("623dbf15-2b07-4e60-82d4-0ae11442ce84", 2019, 3)]
        public async Task GetMonthlyReport_Success(string subscriptionId, int year, int month)
        {
            var request = new GetReportByMonthRequest { SubscriptionId = subscriptionId, Year = year, Month = month };

            var report = await Api.GetReportByMonth(request);

            Assert.IsNotNull(report);
            Assert.AreEqual(new DateTime(year, month, 1), report.StartDate);
            Assert.AreEqual(new DateTime(year, month, DateTime.DaysInMonth(year, month)), report.EndDate);
            Assert.AreEqual(subscriptionId, report.SubscriptionId);
        }
    }
}
