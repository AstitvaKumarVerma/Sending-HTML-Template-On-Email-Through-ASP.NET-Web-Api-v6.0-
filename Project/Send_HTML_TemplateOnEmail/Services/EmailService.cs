using System.Net.Mail;
using System.Net;
using Send_HTML_TemplateOnEmail.Interfaces;

namespace Send_HTML_TemplateOnEmail.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public string SendTemplateToEmails(string Email, string subject, string emailhtml)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var smtpServer = _config["EmailConfiguration:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailConfiguration:Port"]);
                var smtpUsername = _config["EmailConfiguration:Username"];
                var smtpPassword = _config["EmailConfiguration:Password"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress(smtpUsername),
                        To = { new MailAddress(Email) },
                        Subject = subject,
                        Body = $"{emailhtml}",
                        IsBodyHtml = true
                    };

                    client.Send(mail);

                    return "Successful";
                }
            }
            catch (Exception ex)
            {
                return $"Failed to send Template to email. Error: {ex.Message ?? "Unknown error"}";
            }
        }
    }
}
