﻿using Billing.API.SDK;
using System.Threading.Tasks;

namespace Billing.Services.Interfaces
{
    public interface ICostCalculatorService
    {
        Money CalculateServiceCost(string serviceType, int serviceCallCount);
        Money CostPrediction(decimal existingCost, int year, int month);
    }
}
