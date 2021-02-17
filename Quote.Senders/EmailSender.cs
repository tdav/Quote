using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Quote.Senders;
using Quote.Utils;
using System.Net.Mail;
using System.Threading.Tasks;
using Quote.Senders.ViewModels;
using QuoteServer.Extensions;
using Microsoft.Extensions.Logging;

namespace Quote.Senders
{
    public class EmailSender : ISender
    {
        private readonly IConfiguration conf;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(IConfiguration _conf, ILogger<EmailSender> _logger)
        {
            conf = _conf;
            logger = _logger;
        }

        public string Name => "email";

        public async Task<bool> SendAsync(object value)
        {
            viEmailModel email = value as viEmailModel;

            logger.LogInformation($"Send EMAIL to {email.ToEmail}");
            return await ValueTask.FromResult(true);

            var fromEmail = conf["EmailSender:SMTPAccount"];
            var smtpServer = conf["EmailSender:SMTPServer"];
            var smtpPort = conf["EmailSender:SMTPPort"];
            var smtpPassw = conf["EmailSender:SMTPPassword"];


            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin Quote service", fromEmail));
            emailMessage.To.Add(new MailboxAddress("User", email.ToEmail));
            emailMessage.Subject = email.Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = email.Body;

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort.ToInt());
                await client.AuthenticateAsync(fromEmail, smtpPassw);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

            return true;
        }
    }
}
