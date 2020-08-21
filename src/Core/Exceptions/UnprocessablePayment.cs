using System;
namespace Core.Exceptions
{
    public class UnprocessablePayment : Exception
    {
        public override string ToString()
        {
            return "Could not Process Payment";
        }
    }
}