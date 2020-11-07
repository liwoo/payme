using System;
using Core.Services;

namespace Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public Decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public string AgentName { get; set; }
        public string SenderName { get; set; }
        public string BankName { get; set; }
        public string ProviderName { get; set; }
    }

    public enum Bank
    {
        None,
        Standard,
        AirtelMoney,
        Mpamba,
        Missing,
        National
    }
}