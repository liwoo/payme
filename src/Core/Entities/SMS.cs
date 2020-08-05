namespace Core.Entities
{
    public class SMS
    {
        public readonly string Phone;
        public readonly string Message;

        public SMS(string phone, string message)
        {
            Phone = phone;
            Message = message;
        }
    }
}