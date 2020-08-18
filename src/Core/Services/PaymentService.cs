using System.Text.RegularExpressions;
using System;
namespace Core.Services
{
    public class PaymentService
    {
        public Payment GenerateFromMpamba(string number, string content)
        {
            //Look for any text between Amt and MWK
            var amountRegex = new Regex("((?<=(Amount: )|(Amt: ))(.*)(?=M))|((?<=(recieved ))(.*)(?=M))");
            var referenceRegex = new Regex("(?<=Ref: )(.*)(?=)");
            var fromAgentRegex = new Regex("(Cash In)");
            var amount = Decimal.Parse(amountRegex.Match(content).ToString());
            var reference = referenceRegex.Match(content).ToString();
            var fromAgent = fromAgentRegex.IsMatch(content);
            var fromBank = Regex.IsMatch("(Deposit from)", content);

            return new Payment()
            {
                Amount = amount,
                PhoneNumber = number,
                FromAgent = fromAgent,
                FromBank = fromBank,
                Reference = reference
            };
        }
        //TODO public Payment GenerateFromAirtelMonet(string number, string content)
    }

    public class Payment
    {
        public Decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public bool FromAgent { get; set; }
        public bool FromBank { get; set; }
    }
}