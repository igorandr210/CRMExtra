namespace Application.Common.DTOs
{
    public class SendEmailDto
    {
        public string From { get; set; }
        public string Destination { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
