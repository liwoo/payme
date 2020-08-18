using System;
namespace Core.Services
{
    public class ProviderService
    {
        public Provider GetProviderName(string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }

    public enum Provider
    {
        Mpamba,
        AirtelMoney
    }

}