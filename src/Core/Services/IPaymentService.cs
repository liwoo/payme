using Core.Entities;
using Text;

namespace Core.Services
{
    public interface IPaymentService
    {
        bool IsDeposit();
        bool HasInvalidReference();
        Payment GeneratePayment();

        string CreateReference(); 

        static string SanitizeMessage(string message)
        {
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