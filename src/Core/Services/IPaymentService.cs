using Core.Entities;

namespace Core.Services
{
    interface IPaymentService
    {
        bool IsDeposit();
        bool HasInvalidReference();
        Payment GeneratePayment();

        static string SanitizeMessage(string message)
        {
            //TODO: sanitize message here
            return message;
        }

        static Bank GetBankNameFromString(string bankName)
        {
            return bankName switch
            {
                "STANDARD BANK" => Bank.Standard,
                "AIRTEL MONEY" => Bank.AirtelMoney,
                _ => Bank.None
            };
        }
    }

}