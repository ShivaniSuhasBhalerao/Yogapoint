using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace TestLoginYogapoint.Emailing
{
    public  class EmailService : TestLoginYogapointAppService,ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;

        public EmailService(IEmailSender emailSender, ITemplateRenderer templateRenderer)
        {
            _emailSender = emailSender;
            _templateRenderer = templateRenderer;
        }
        public  async Task SendAsync(string targetEmail)
                
        {
            var emailBody = await _templateRenderer.RenderAsync(
                StandardEmailTemplates.Message,
                new
                {
                    message = " We received an account recovery request! If you initiated this request, click the following link to reset your password " +
                    "https://localhost:44335/Account/ResetPassword?userId=006f63e5-1a7d-cba4-e0a7-3a0792b7866e&__tenant=&resetToken=CfDJ8LiFVAOnpkBNpF5PY4a3FtUlrFV5bt7RhtjzgL%2FbCXUs5vgVE7m6EVruwADVcuDTdVIVnBlhKRmufQAKDt4rukgXnwl5G3MIolHP5ktf9LkoQk%2Bb1d8un0QmJ6FXMGbH%2BTKkQ8q1KCM5csG6qFqGkVv0j%2FSSLC3sPTuP7%2BWats1155lSx6HSg0rveJIglGiDwv%2BDVWkzCTGuDdVAlMNUJq1lfEEtH0WbM2GNMwzy7asj&returnUrl=/connect/authorize?response_type=code&client_id=TestLoginYogapoint_App&state=LndUTVhGUmFVNXFzVHkya205dG5teHBnSVdsRm9sSHYtWDlubmUtRzlKUi5s&redirect_uri=http%3A%2F%2Flocalhost%3A4200&scope=openid%20offline_access%20TestLoginYogapoint&code_challenge=pwo2LSec2H84s9MXaPg46Dg8fUUtfLHogymcm1U7Jsg&code_challenge_method=S256&nonce=LndUTVhGUmFVNXFzVHkya205dG5teHBnSVdsRm9sSHYtWDlubmUtRzlKUi5s&culture=en&ui-culture=en"
                }
               ); 
            await _emailSender.SendAsync(targetEmail, "Subject", emailBody
                );
        }
    }
}

