namespace Send_HTML_TemplateOnEmail.Interfaces
{
    public interface IEmailService
    {
        public string SendTemplateToEmails(string Email, string Subject, string emailhtml);
    }
}
