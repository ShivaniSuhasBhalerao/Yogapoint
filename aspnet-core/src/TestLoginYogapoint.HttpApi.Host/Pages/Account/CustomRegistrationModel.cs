using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using TestLoginYogapoint.UserExtraProperties;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
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

        public CustomRegistrationModel(IAccountAppService accountAppService) : base(accountAppService)
        {
            Input = new();
            Input.UserName = Faker.Name.First();
            Input.PhoneNumber = Faker.RandomNumber.Next(10).ToString();
            Input.EmailAddress = Faker.Internet.Email(Input.UserName);
            Input.Gender = (char)Faker.Enum.Random<Gender>();
        }

        public class IdentityUserExtraProperties : IdentityUser
        {
            public IdentityUserExtraProperties(Guid id, string userName, string email, string _PhoneNumber, Guid? tenantId = null) : base(id, userName, email, tenantId)
            {
                PhoneNumber = _PhoneNumber;
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
                    UserName = Input.EmailAddress,
                    PhoneNumber = Input.PhoneNumber,
                    Gender = Input.Gender,


                }
            );

            var user = await UserManager.GetByIdAsync(userDto.Id);
            await SignInManager.SignInAsync(user, isPersistent: true);
        }
        public virtual async Task<IdentityUserDto> RegisterAsync(CustRegDTO input)
        {
            await CheckSelfRegistrationAsync();


            await IdentityOptions.SetAsync();

            var user = new IdentityUserExtraProperties(GuidGenerator.Create(), input.UserName, input.EmailAddress, input.PhoneNumber, CurrentTenant.Id);


            user.SetProperty(AbpUserExtraProperties.GenderPropertyTitle, input.Gender.ToString());
            input.MapExtraPropertiesTo(user);




            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
            (await UserManager.AddDefaultRolesAsync(user)).CheckErrors();



            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
        }

    }


    public class CustRegDTO : RegisterDto
    {


        public char Gender { get; set; }
        public string PhoneNumber { get; set; }
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

        public string PhoneNumber { get; set; }
    }
}