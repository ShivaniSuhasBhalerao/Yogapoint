using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp;
using Volo.Abp.Account.Web.Pages.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Castle.Core.Smtp;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Volo.Abp.DependencyInjection;
using static TestLoginYogapoint.Pages.Account.CustomForgotPassword;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using static TestLoginYogapoint.Pages.Account.CustomRegistrationModel;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Account.Settings;
using Volo.Abp.Settings;

namespace TestLoginYogapoint.Pages.Account
{
 
    public class CustomForgotPassword : ForgotPasswordModel
    {
        protected IdentityUserManager _UserManager;
        protected IAccountEmailer AccountEmailer { get; }


        public CustomForgotPassword(IdentityUserManager userManager,IAccountEmailer accountEmailer) : base()
        {
            _UserManager = userManager;
            AccountEmailer=accountEmailer;
        }
        public override Task<IActionResult> OnGetAsync()
        {
            return Task.FromResult<IActionResult>(Page());
        }


        protected virtual async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            var users = await UserManager.FindByEmailAsync(email);
            if (users == null)
            {
                throw new UserFriendlyException(L["Volo.Account:InvalidEmailAddress", email]);
            }

          

            return users;
        }
     
        public virtual async Task SendPasswordResetCodeAsync(SendPasswordResetCodeDto input)
        {
            var user = await GetUserByEmailAsync(input.Email);
            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user);
             await AccountEmailer.SendPasswordResetLinkAsync(user, resetToken, input.AppName, input.ReturnUrl, input.ReturnUrlHash);
        }



        

   

        public override async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await SendPasswordResetCodeAsync(
                    new SendPasswordResetCodeDto
                    {
                        Email = Email,
                        AppName = "MVC", //TODO: Const!
                        ReturnUrl = ReturnUrl,
                        ReturnUrlHash = ReturnUrlHash
                    }
                );
            }
            catch (UserFriendlyException e)
            {
                Alerts.Danger(GetLocalizeExceptionMessage(e));
                return Page();
            }


            return RedirectToPage(
                "./PasswordResetLinkSent",
                new
                {
                    returnUrl = ReturnUrl,
                    returnUrlHash = ReturnUrlHash
                });
        }




    }
    
}

