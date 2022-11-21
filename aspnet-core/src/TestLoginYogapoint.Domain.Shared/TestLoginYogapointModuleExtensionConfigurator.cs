using System.ComponentModel.DataAnnotations;
using TestLoginYogapoint.UserExtraProperties;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace TestLoginYogapoint;

public static class TestLoginYogapointModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    { }
    private static void ConfigureExtraProperties()
    {
        ObjectExtensionManager.Instance.Modules().
           ConfigureIdentity(identity =>
           {
               identity.ConfigureUser(user =>
           {
               
               user.AddOrUpdateProperty<char>(AbpUserExtraProperties.GenderPropertyTitle, options =>
               {
                   options.DefaultValue = "";
                   options.Attributes.Add(new StringLengthAttribute(AbpUserExtraProperties.MaxLengthGender));
               });
           });
           });
    }
}
