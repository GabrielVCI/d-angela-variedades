using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace d_angela_variedades.Servicios
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implement your email sending logic here, using SMTP or any other service
            try
            {
                string smtpServer = _configuration["SmtpSettings:Server"];
                int smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
                string smtpUsername = _configuration["SmtpSettings:Username"];
                string smtpPassword = _configuration["SmtpSettings:Password"];
                bool enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = enableSsl;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpUsername, "AngelaVariedades")
                    };
                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessage;
                    mailMessage.IsBodyHtml = true;

                    await client.SendMailAsync(mailMessage);


                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions, log, etc.
                throw new ApplicationException($"Error sending email: {ex.Message}");

            }
        }
    }
}
