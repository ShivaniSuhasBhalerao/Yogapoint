using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using TestLoginYogapoint;
using TestLoginYogapoint.Email;

namespace BOOKSTore.Email
{
    public class EmailService :  IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public bool SendEmail(EmailData emailData)
        {
            try
            {
                var Bodyhtml = @"<!DOCTYPE html>
               <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
               <head>
               <meta charset=""utf-8"" />
               </head>
               <body>
               <h3>Password reset</h3>
    
              <p>We received an account recovery request! If you initiated this request, click the following link to reset your password.</p>
    
                <div>
        <a href=""https://localhost:44335/Account/ResetPassword?userId=006f63e5-1a7d-cba4-e0a7-3a0792b7866e&__tenant=&resetToken=CfDJ8LiFVAOnpkBNpF5PY4a3FtXnNZ0E823it6dglX1KRMViRJZJzVWST6%2FDb5tHtNZFrzZqLU1REqwH3Fw9WI4Ii5VHLEd%2BFi7d9o8q5uaF5aNEG9WdTgB0TjX2WlLbhmpmIEB9GmZO2B5XSQT2c2qm7jcjJrWlabIstXdIVpaRcpckNSHYJMysqSDW08q1mkJpwucTRp6HzdwTS9LcSuANP6Iv77mxPyVILcmj6izOJiKX&returnUrl=/connect/authorize?response_type=code&client_id=TestLoginYogapoint_App&state=dGd0MEJuUmR4dlBqMERxT1NZRGZlQWNPcS5ndU5lZVVIMkZ0TUhHekxKdnd0&redirect_uri=http%3A%2F%2Flocalhost%3A4200&scope=openid%20offline_access%20TestLoginYogapoint&code_challenge=_jKD49C8_TCmLnaJzIUK8JxLKPQaDKGlng_gHZQqrUE&code_challenge_method=S256&nonce=dGd0MEJuUmR4dlBqMERxT1NZRGZlQWNPcS5ndU5lZVVIMkZ0TUhHekxKdnd0&culture=en&ui-culture=en"">Reset my password</a>
        </div>
      </body>
</html>";
                MimeMessage emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_emailSettings.Name, _emailSettings.EmailId);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(emailData.EmailToName, emailData.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = emailData.EmailSubject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                //emailBodyBuilder.TextBody = emailData.EmailBody;
                emailMessage.Body = new TextPart("html") { Text = Bodyhtml };


                emailBodyBuilder.ToMessageBody();
             

                SmtpClient emailClient = new SmtpClient();

                emailClient.Connect(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                emailClient.Authenticate(_emailSettings.EmailId, _emailSettings.Password);
                emailClient.Send(emailMessage);
                emailClient.Disconnect(true);
                emailClient.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                //Log Exception Details
                return false;
            }
        }
    }
}
