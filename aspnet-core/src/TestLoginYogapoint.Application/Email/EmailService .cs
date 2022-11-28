using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
using TestLoginYogapoint;
using TestLoginYogapoint.Email;

namespace BOOKSTore.Email
{
    public class EmailService : TestLoginYogapointAppService, IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task<bool> SendEmailAsync(EmailData emailData)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();
                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = emailData.EmailSubject;
                BodyBuilder emailBodyBuilder = new BodyBuilder();
                if (emailData.IshtmlTemplet)
                    emailMessage.Body = new TextPart("html") { Text = emailData.EmailBody };
                else
                {
                    emailBodyBuilder.TextBody = emailData.EmailBody;

                    emailMessage.Body = emailBodyBuilder.ToMessageBody();
                }
                emailBodyBuilder.ToMessageBody();

                SmtpClient emailClient = new SmtpClient();
                await emailClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                await emailClient.AuthenticateAsync(_emailSettings.EmailId, _emailSettings.Password);
                await emailClient.SendAsync(emailMessage);
                await emailClient.DisconnectAsync(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
