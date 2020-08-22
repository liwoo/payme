using System;
using System.Text.RegularExpressions;
using Core.Entities;

namespace Core.Services
{
    public class MpambaService : IPaymentService
    {
        public readonly string _message;
        public readonly string _phoneNumber;

        public MpambaService(string message, string phoneNumber)
        {
            _message = IPaymentService.SanitizeMessage(message);
            _phoneNumber = phoneNumber;
        }

        public Payment GeneratePayment()
        {
            var amountRegex = new Regex("((?<=(Amount:)|(Amt:))(.*?)(?=M))|((?<=(recieved))(.*?)(?=M))");
            var referenceRegex = new Regex(@"(?<=Ref:)(.*?)(?=((Bal)|(Avai)))");
            var bankNameRegex = new Regex("(?<=from)(.*?)(?=on)");
            var fromAgentRegex = new Regex("(CashIn)");
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
                ProviderName = Provider.Mpamba
            };
        }

        public bool HasInvalidReference()
        {
            var reference = Regex.Match(_message,@"(?<=Ref:)(.*?)(?=((Bal)|(Avai)))").ToString().Trim();
            return (reference.Length == 10);
        }

        public bool IsDeposit()
        {
            return Regex.IsMatch(_message, @"((CashIn)|(Received)|(recieved)|(Deposit))");
        }
    }
}