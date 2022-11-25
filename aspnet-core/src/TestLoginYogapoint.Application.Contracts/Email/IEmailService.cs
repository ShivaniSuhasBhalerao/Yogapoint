using System;
using System.Collections.Generic;
using System.Text;

namespace TestLoginYogapoint.Email
{
    public interface IEmailService
    {
        bool SendEmail(EmailData emailData);
    }
}
