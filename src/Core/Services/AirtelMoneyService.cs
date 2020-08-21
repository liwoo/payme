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
            throw new NotImplementedException();
        }

        public bool HasInvalidReference()
        {
            throw new NotImplementedException();
        }

        public bool IsDeposit()
        {
            throw new NotImplementedException();
        }
    }
}