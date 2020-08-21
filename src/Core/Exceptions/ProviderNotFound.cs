using System;
namespace Core.Exceptions
{
    public class ProviderNotFound : Exception
    {
        public override string ToString()
        {
            return "Provider Not Found";
        }
    }
}