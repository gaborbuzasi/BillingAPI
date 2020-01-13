using Billing.API.SDK;
using Billing.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Billing.API.Tests
{
    [TestClass]
    public class ServiceTests : IntegrationTestsBase
    {
        [DataTestMethod]
        [DataRow("invoice", 1, 0.15)]
        [DataRow("mail", 1, 0.01)]
        [DataRow("thumbnail", 1, 0.10)]
        [DataRow("template", 1, 0.05)]
        [DataRow("document", 1, 0.20)]
        [DataRow("feed", 1, 0.12)]
        [DataRow("payment", 1, 0.11)]
        [DataRow("checkout", 1, 0.09)]
        [DataRow("invoice", 15, 0.15)]
        [DataRow("mail", 15, 0.01)]
        [DataRow("thumbnail", 15, 0.10)]
        [DataRow("template", 15, 0.05)]
        [DataRow("document", 15, 0.20)]
        [DataRow("feed", 15, 0.12)]
        [DataRow("payment", 15, 0.11)]
        [DataRow("checkout", 15, 0.09)]
        public void ServiceCostCalculator_CorrectValues_Success(string serviceType, int serviceCallCount, double expectedSingleCallCost)
        {
            var costCalculator = ApiServer.Services.GetService<ICostCalculatorService>();

            var cost = costCalculator.CalculateServiceCost(serviceType, serviceCallCount);

            Assert.IsNotNull(cost);

            // Cost is hard coded to be EUR
            Assert.AreEqual(CurrencyIso.EUR, cost.Currency);
            Assert.AreEqual(Convert.ToDecimal(expectedSingleCallCost) * serviceCallCount, cost.Amount);
        }
    }
}
