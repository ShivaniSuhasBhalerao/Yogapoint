using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestLoginYogapoint.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailData emailData);
    }
}
