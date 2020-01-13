using Billing.API.SDK;
using Billing.Services.Interfaces;
using System;

namespace Billing.Services
{
    public class CostCalculatorService : ICostCalculatorService
    {
        public Money CalculateServiceCost(string serviceType, int serviceCallCount)
        {
            var serviceCost = new Money { Currency = CurrencyIso.EUR };

            switch (serviceType)
            {
                case "invoice":
                    serviceCost.Amount = 0.15m * serviceCallCount;
                    break;
                case "mail":
                    serviceCost.Amount = 0.01m * serviceCallCount;
                    break;
                case "thumbnail":
                    serviceCost.Amount = 0.10m * serviceCallCount;
                    break;
                case "template":
                    serviceCost.Amount = 0.05m * serviceCallCount;
                    break;
                case "document":
                    serviceCost.Amount = 0.20m * serviceCallCount;
                    break;
                case "feed":
                    serviceCost.Amount = 0.12m * serviceCallCount;
                    break;
                case "payment":
                    serviceCost.Amount = 0.11m * serviceCallCount;
                    break;
                case "checkout":
                    serviceCost.Amount = 0.09m * serviceCallCount;
                    break;
                default:
                    throw new NotImplementedException($"Service type {serviceType} cost calculation is not supported");
            }

            return serviceCost;
        }
    }
}
