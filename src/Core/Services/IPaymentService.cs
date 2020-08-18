using Core.Entities;

namespace Core.Services
{
    interface IPaymentService
    {
        bool IsDeposit();
        bool HasInvalidReference();
        Payment GeneratePayment();

        static Bank GetBankNameFromString(string bankName)
        {
            return bankName switch
            {
                "STANDARD BANK" => Bank.Standard,
                _ => Bank.None
            };
        }
    }

}