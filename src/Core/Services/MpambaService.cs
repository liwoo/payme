using System;
using System.Text.RegularExpressions;
using Core.Entities;

namespace Core.Services
{
    public class MpambaService : IPaymentService
    {
        private readonly string _message;
        private readonly string _phoneNumber;

        public MpambaService(string message, string phoneNumber)
        {
            _message = message;
            _phoneNumber = phoneNumber;
        }

        public Payment GeneratePayment()
        {
            var amountRegex = new Regex("((?<=(Amount: )|(Amt: ))(.*?)(?=M))|((?<=(recieved ))(.*?)(?=M))");
            var referenceRegex = new Regex(@"(?<=Ref: )(.*?)(?=\s)");
            var bankNameRegex = new Regex("(?<=from )(.*?)(?= on)");
            var fromAgentRegex = new Regex("(Cash In)");
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
            throw new System.NotImplementedException();
        }

        public bool IsDeposit()
        {
            throw new System.NotImplementedException();
        }
    }
}