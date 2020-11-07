using Newtonsoft.Json;

namespace Application.SMSs.DTOs
{
    public class SMSBodyDto
    {
        public string Phone { get; set; }

        public string Text { get; set; }
    }
}