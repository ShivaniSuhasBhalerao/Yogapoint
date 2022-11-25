using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using TestLoginYogapoint.UserExtraProperties;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Settings;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using static TestLoginYogapoint.Pages.Account.CustomRegistrationModel;
using static Volo.Abp.Account.Web.Pages.Account.RegisterModel;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
namespace TestLoginYogapoint.Pages.Account
{
    public class CustomRegistrationModel : RegisterModel
    {
        [BindProperty]
        public Inputdata Input { get; set; }

        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly ILookupNormalizer _lookupNormalizer;
        public CustomRegistrationModel(IAccountAppService accountAppService, IIdentityUserRepository identityUserRepository, ILookupNormalizer lookupNormalizer) : base(accountAppService)
        {
            Input = new();
            Input.Name = Faker.Name.First();
            Input.Surname = Faker.Name.Last();
            Input.UserName = Faker.Name.First();

            Input.EmailAddress = Faker.Internet.Email(Input.UserName);
            Input.Gender = (char)Faker.Enum.Random<Gender>();
            _identityUserRepository =identityUserRepository;
            _lookupNormalizer=lookupNormalizer;
        }

        

        

        public class IdentityUserExtraProperties : IdentityUser
        {
            public IdentityUserExtraProperties(Guid id, string userName, string email,string _Name,string _Surname, Guid? tenantId = null) : base(id, userName, email, tenantId)
            {
               
                Name = _Name;
                Surname = _Surname;
            }
        }
        protected override async Task RegisterLocalUserAsync()
        {
            ValidateModel();

            var userDto = await RegisterAsync(
                new CustRegDTO
                {
                    AppName = "MVC",
                    EmailAddress = Input.EmailAddress,
                    Password = Input.Password,
                    UserName = Input.Name,
                
                    Gender = Input.Gender,
                    Name= Input.Name,
                    Surname=Input.Surname,


                }
            );

            var user = await UserManager.GetByIdAsync(userDto.Id);
            await SignInManager.SignInAsync(user, isPersistent: true);
        }
        public virtual async Task<IdentityUserDto> RegisterAsync(CustRegDTO input)
        {
            await CheckSelfRegistrationAsync();


            await IdentityOptions.SetAsync();

            var user = new IdentityUserExtraProperties(GuidGenerator.Create(), input.UserName, input.EmailAddress,input.Name,input.Surname, CurrentTenant.Id);


            user.SetProperty(AbpUserExtraProperties.GenderPropertyTitle, input.Gender.ToString());
            input.MapExtraPropertiesTo(user);


            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();



            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }
        //protected virtual async Task CheckSelfRegistrationAsync()
        //{
        //    if (!await SettingProvider.IsTrueAsync(AccountSettingNames.IsSelfRegistrationEnabled))
        //    {
        //        throw new UserFriendlyException(L["SelfRegistrationDisabledMessage"]);
        //    }
        //}

        //public override async Task<IActionResult> OnPostAsync()
        //{
        //    try
        //    {
        //        await CheckSelfRegistrationAsync();

        //        if (IsExternalLogin)
        //        {
        //            var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();
        //            if (externalLoginInfo == null)
        //            {
        //                Logger.LogWarning("External login info is not available");
        //                return RedirectToPage("./Login");
        //            }

        //            await RegisterExternalUserAsync(externalLoginInfo, Input.EmailAddress);
        //        }
        //        else
        //        {
        //            await RegisterLocalUserAsync();
        //        }

        //        return Redirect(ReturnUrl ?? "~/"); 
        //    }
        //    catch (BusinessException e)
        //    {
        //        Alerts.Danger(GetLocalizeExceptionMessage(e));
        //        return Page();
        //    }
        //}

        public override async Task<IActionResult> OnPostAsync()
        {
            var result = await base.OnPostAsync();
           var user = await _identityUserRepository.FindByNormalizedEmailAsync(_lookupNormalizer.NormalizeEmail(Input.EmailAddress));
            user.Name = Input.Name;
            user.Surname = Input.Surname;

         await _identityUserRepository.UpdateAsync(user);
         return result;



        }
        //public async override Task<IActionResult> OnPostAsync()
        //{

        //    var data = base.OnPostAsync();
        //    return RedirectToPage("./Login", data);

        //}

    }




    public class CustRegDTO : RegisterDto
    {


        public char Gender { get; set; }
       

        public string Name { get; set; }
        public string Surname { get; set; }

    }


    public enum Gender
    {
        Male = 'M',
        Female = 'F'
    }

    public class Inputdata : PostInput
    {

        public char Gender { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [NotMapped]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

     

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }


    }
}