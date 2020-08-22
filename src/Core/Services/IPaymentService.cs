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
            return TextUtils.RemoveInPlaceCharArray(message);
        }

        static Bank GetBankNameFromString(string bankName)
        {
            return bankName switch
            {
                "STANDARDBANK" => Bank.Standard,
                "AIRTELMONEY" => Bank.AirtelMoney,
                "BankAccount" => Bank.Missing,
                _ => Bank.None
            };
        }
    }

}