using System;
using System.Text.RegularExpressions;
using Core.Entities;

namespace Core.Services
{
    public class AirtelMoneyService : IPaymentService
    {
        public readonly string _message;
        public readonly string _phoneNumber;
        
        public AirtelMoneyService(string message, string phoneNumber)
        {
            _message = IPaymentService.SanitizeMessage(message);
            _phoneNumber = phoneNumber;
        }
        public Payment GeneratePayment()
        {
            var amountRegex = new Regex(@"((?<=((receivedMK)|(recievedMK)))(.*?)(?=from))");
            var referenceRegex = new Regex(@"(?<=(Id:)|(ID:))(.*?)(?=((\.De)|(Yo)))");
            var bankNameRegex = new Regex(@"(?<=from)(.*?)(?=\.)");
            var fromAgentRegex = new Regex("(Txn)");
            var amount = Decimal.Parse(amountRegex.Match(_message).ToString().Trim());
            var reference = referenceRegex.Match(_message).ToString();
            var fromAgent = fromAgentRegex.IsMatch(_message);
            var BankName = bankNameRegex.Match(_message).ToString();

            return new Payment()
            {
                Amount = amount,
                PhoneNumber = _phoneNumber,
                FromAgent = fromAgent,
                Reference = reference,
                BankName = IPaymentService.GetBankNameFromString(BankName),
                ProviderName = Provider.AirtelMoney
            };
        }

        public bool HasInvalidReference()
        {
            return Regex.IsMatch(_message,@"[(A-Z)|(A-Z)|(0-9)]{8}.[0-9]{4}.[(A-Z)|(A-Z)|(0-9)]{6}");
        }

        public bool IsDeposit()
        {
            return Regex.IsMatch(_message, @"((recieved)|(received))");
        }
    }
}