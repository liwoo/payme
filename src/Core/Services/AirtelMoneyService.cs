using System;
using System.Text.RegularExpressions;
using Core.Entities;

namespace Core.Services
{
    public class AirtelMoneyService : IPaymentService
    {
        private readonly string _message;
        private readonly string _phoneNumber;

        public AirtelMoneyService(string message, string phoneNumber)
        {
            _message = IPaymentService.SanitizeMessage(message);
            _phoneNumber = phoneNumber;
        }
        public Payment GeneratePayment()
        {
            var amountRegex = new Regex(@"((?<=((receivedMK)|(recievedMK)))(.*?)(?=from))");
            var ger = amountRegex.Match(_message).ToString();
            var amount = Decimal.Parse(amountRegex.Match(_message).ToString());
            var reference = CreateReference();
            var SenderName = Regex.Match(GetSenderName(), @"(?<=.*(?=[a-z]|[A-Z])).*").ToString();

            return new Payment()
            {
                Amount = amount,
                PhoneNumber = _phoneNumber,
                AgentName = "Missing",
                Reference = reference,
                SenderName = SenderName,
                BankName = IPaymentService.GetBankNameFromString(GetSenderName()).ToString(),
                ProviderName = Provider.AirtelMoney.ToString()
            };
        }

        public bool HasInvalidReference()
        {
            return !Regex.IsMatch(_message, "[(A-Z)|(A-Z)|(0-9)]{8}.[0-9]{4}.[(A-Z)|(A-Z)|(0-9)]{6}");
        }

        public bool IsDeposit()
        {
            return Regex.IsMatch(_message, "((recieved)|(received))");
        }

        public string CreateReference()
        {
            return Regex.Match(_message, @"(?<=(Id:)|(ID:))(.*?)(?=((\.De)|(Yo)))").ToString().Trim();
        }

        private String GetSenderName() {
            return Regex.Match(_message,@"(?<=from)(.*?)(?=\.)").ToString();
        }
    }
}