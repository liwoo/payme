namespace Application.Common.Models
{
    public class SMSContents
    {
        public string Phone { get; set; }
        public string Contents { get; set; }

        static string SanitezeContent(string contents) {
            return contents;
        }
    }
}