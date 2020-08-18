using System;
using Core.Services;

namespace Core.Entities
{
    public class Payment
    {
        public Decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public bool FromAgent { get; set; }
        public Bank BankName { get; set; }
        public Provider ProviderName { get; set; }
    }

    public enum Bank
    {
        None,
        Standard,
        National
    }
}