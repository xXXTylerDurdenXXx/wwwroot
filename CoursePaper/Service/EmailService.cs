
using System.Net;
using System.Net.Mail;

namespace CoursePaper.Service
{
    public class EmailServices : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailServices(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var smtp = new SmtpClient()
            {
                Host = _config["Mail:Host"],
                Port = int.Parse(_config["Mail:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                _config["Mail:User"],
                _config["Mail:Password"])
            };

            var mail = new MailMessage()
            {
                From = new MailAddress(_config["Mail:User"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            await smtp.SendMailAsync(mail);
        }
    }
}
