using System.Text.RegularExpressions;
using System;
using Core.Entities;

namespace Core.Services
{
    public class PaymentService
    {
        public Payment GenerateFromMpamba(string number, string content)
        {
            //Look for any text between Amt and MWK
            var amountRegex = new Regex("((?<=(Amount: )|(Amt: ))(.*?)(?=M))|((?<=(recieved ))(.*?)(?=M))");
            var referenceRegex = new Regex(@"(?<=Ref: )(.*?)(?=\s)");
            var bankNameRegex = new Regex("(?<=from )(.*?)(?= on)");
            var fromAgentRegex = new Regex("(Cash In)");
            var amount = Decimal.Parse(amountRegex.Match(content).ToString().Trim());
            var reference = referenceRegex.Match(content).ToString();
            var fromAgent = fromAgentRegex.IsMatch(content);
            var BankName = bankNameRegex.Match(content).ToString();

            //TODO: public GetServiceProviderFromPhoneNumber()
            // enum Service Proiveders
            //
            //TODO: public IsValidRefernce()
            //TODO: public IsDeposit()
            //throw  InvalidReferenceException Core > Exceptions > 

            return new Payment()
            {
                Amount = amount,
                PhoneNumber = number,
                FromAgent = fromAgent,
                Reference = reference,
                BankName = this.GetBankNameFromString(BankName)
            };
        }

        private Bank GetBankNameFromString(String bankName)
        {
            return bankName switch
            {
                "STANDARD BANK" => Bank.Standard,
                _ => Bank.None
            };
        }
    }
}