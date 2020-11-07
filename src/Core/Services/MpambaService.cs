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
            _phoneNumber = IPaymentService.SanitizePhoneNumber(phoneNumber);
        }

        public Payment GeneratePayment()
        {
            var amountRegex = new Regex("((?<=(Amount:)|(Amt:))(.*?)(?=M))|((?<=(recieved))(.*?)(?=M))");
            var amount = Decimal.Parse(amountRegex.Match(_message).ToString().Trim());
            var reference = CreateReference();
            var agentName = Regex.IsMatch(_message, "(CashIn)") ? GetSenderName() : "Missing";
            var SenderName = Regex.Match(GetSenderName(), @"(?<=.*(?=[a-z]|[A-Z])).*").ToString();

            return new Payment()
            {
                Amount = amount,
                PhoneNumber =  _phoneNumber,
                AgentName = agentName,
                Reference = reference,
                SenderName = SenderName,
                BankName = IPaymentService.GetBankNameFromString(GetSenderName()).ToString(),
                ProviderName = Provider.Mpamba.ToString()
            };
        }

        public bool HasInvalidReference()
        {
            return (CreateReference().Length != 10);
        }

        public bool IsDeposit()
        {
            return Regex.IsMatch(_message, @"((CashIn)|(Received)|(recieved)|(Deposit))");
        }

        public string CreateReference()
        {
            return new Regex(@"(?<=Ref:)(.*?)(?=((Bal)|(Avai)))").Match(_message).ToString();
        }

        private String GetSenderName()
        {
            return Regex.Match(_message, @"(?<=from)(.*?)(?=on)").ToString();
        }
    }
}