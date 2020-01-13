using Billing.API.SDK;
using Billing.Common;
using Billing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RequestDataCollector.SDK;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Billing.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IRequestDataCollectorApi RequestDataCollectorApi { get; }
        ICostCalculatorService CostCalculatorService { get; }

        public ReportController(IRequestDataCollectorApi requestDataCollectorApi,
                                ICostCalculatorService costCalculatorService)
        {
            RequestDataCollectorApi = requestDataCollectorApi;
            CostCalculatorService = costCalculatorService;
        }

        [HttpGet("{SubscriptionId}/{Year}/{Month}")]
        public async Task<GetReportByMonthResponse> GetReportByMonth([FromModel] GetReportByMonthRequest request)
        {
            // This could be potentially mapped/converted using a mapper
            var apiRequest = new GetRequestDataRequest { Month = request.Month, Year = request.Year };

            // pagination would be required
            var apiResult = await RequestDataCollectorApi.GetRequestData(apiRequest);

            var subscriptionData = apiResult.Requests.Where(req => req.SubscriptionId == request.SubscriptionId);

            var serviceCallCountsByType = subscriptionData.GroupBy(req => req.ServiceName).ToDictionary(req => req.Key, req => req.Count());

            var totalCost = 0m;

            foreach (var serviceCallCount in serviceCallCountsByType)
            {
                totalCost += CostCalculatorService.CalculateServiceCost(serviceCallCount.Key, serviceCallCount.Value).Amount;
            }

            var daysInCurrentMonth = DateTime.DaysInMonth(request.Year, request.Month);

            var result = new GetReportByMonthResponse
            {
                StartDate = new DateTime(request.Year, request.Month, 1),
                EndDate = new DateTime(request.Year, request.Month, daysInCurrentMonth),
                SubscriptionId = request.SubscriptionId,
                TotalCost = new Money { Currency = CurrencyIso.EUR, Amount = totalCost },
                TotalNumberOfRequests = serviceCallCountsByType.Sum(scc => scc.Value)
            };

            var isCurrentMonthReport = DateTime.Now.Date == new DateTime(request.Year, request.Month, DateTime.Now.Day);
            var elapsedDays = DateTime.Now.Day;

            if (isCurrentMonthReport)
            {
                // calculate proportion elapsed from month in terms of days
                var remainingDaysInMonth = daysInCurrentMonth - elapsedDays;
                var predictionFactor = (decimal)remainingDaysInMonth / elapsedDays;

                result.EstimatedCost = new Money
                {
                    // predicted amount will be cost already incurred + projected usage based already incurred cost
                    Amount = Math.Round(totalCost + predictionFactor * totalCost, 2),
                    Currency = CurrencyIso.EUR
                };
            }

            return result;
        }
    }
}