using Core.Entities;
using Core.Utils;

namespace Core.Services
{
    public interface IPaymentService
    {
        bool IsDeposit();
        bool HasInvalidReference();
        Payment GeneratePayment();

        static string SanitizeMessage(string message)
        {
            //TODO: sanitize message here
            return TextUtils.RemoveWhitespace(message);
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