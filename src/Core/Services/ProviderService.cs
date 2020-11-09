using System;
using System.Text.RegularExpressions;

namespace Core.Services
{
    public class ProviderService
    {
        public Provider GetProviderName(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, "(^26588)|(^88)") ? Provider.Mpamba : Provider.AirtelMoney;
        }

        public IPaymentService ServiceFromProviderFactory(Provider provider, String phone, String message)
        {
            return provider switch
            {
                Provider.Mpamba => new MpambaService(message, phone),
                _ => new AirtelMoneyService(message, phone)
            };
        }
    }

    public enum Provider
    {
        Mpamba,
        AirtelMoney,
        None
    }

}