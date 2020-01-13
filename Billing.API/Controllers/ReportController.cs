﻿using Billing.API.SDK;
using Billing.Common;
using RequestDataCollector.SDK;
using Billing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

            var serviceCallCounts = subscriptionData.GroupBy(req => req.ServiceName).ToDictionary(req => req.Key, req => req.Count());

            var totalCost = 0m;

            foreach (var serviceCallCount in serviceCallCounts)
            {
                totalCost += CostCalculatorService.CalculateServiceCost(serviceCallCount.Key, serviceCallCount.Value).Amount;
            }

            var result = new GetReportByMonthResponse
            {
                StartDate = new DateTime(request.Year, request.Month, 1),
                EndDate = new DateTime(request.Year, request.Month, DateTime.DaysInMonth(request.Year, request.Month)),
                SubscriptionId = request.SubscriptionId,
                TotalCost = new Money { Currency = CurrencyIso.EUR, Amount = totalCost },
                TotalNumberOfRequests = serviceCallCounts.Sum(scc => scc.Value)
            };

            return result;
        }
    }
}