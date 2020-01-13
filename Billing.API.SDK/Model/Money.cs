using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.API.SDK
{
    public class Money
    {
        public decimal Amount { get; set; }
        public CurrencyIso Currency { get; set; }
    }
}
