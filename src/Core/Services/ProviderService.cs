using System;
namespace Core.Services
{
    public class ProviderService
    {
        public Provider GetProviderName(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public IPaymentService ServiceFromProviderFactory(Provider provider, String phone, String message)
        {
            return provider switch
            {
                Provider.Mpamba => new MpambaService(message, phone),
                _ => new MpambaService(message, phone)
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