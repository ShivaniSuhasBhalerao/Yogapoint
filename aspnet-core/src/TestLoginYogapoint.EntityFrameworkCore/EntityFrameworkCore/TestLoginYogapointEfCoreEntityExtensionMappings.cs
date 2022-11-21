using Microsoft.EntityFrameworkCore;
using TestLoginYogapoint.UserExtraProperties;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace TestLoginYogapoint.EntityFrameworkCore;

public static class TestLoginYogapointEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        TestLoginYogapointGlobalFeatureConfigurator.Configure();
        TestLoginYogapointModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            /* You can configure extra properties for the
             * entities defined in the modules used by your application.
             *
             * This class can be used to map these extra properties to table fields in the database.
             *
             * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
             * USE TestLoginYogapointModuleExtensionConfigurator CLASS (in the Domain.Shared project)
             * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
             *
             * Example: Map a property to a table field:

                 ObjectExtensionManager.Instance
                     .MapEfCoreProperty<IdentityUser, string>(
                         "MyProperty",
                         (entityBuilder, propertyBuilder) =>
                         {
                             propertyBuilder.HasMaxLength(128);
                         }
                     );

             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
             */
            ObjectExtensionManager.Instance
             .MapEfCoreProperty<IdentityUser, char>(AbpUserExtraProperties.GenderPropertyTitle,
            (entityBuilder, propertyBuilder) =>
            {
                propertyBuilder.HasDefaultValue('M');
                propertyBuilder.HasMaxLength(AbpUserExtraProperties.MaxLengthGender);
                entityBuilder.HasCheckConstraint("Gender_CK", $"{AbpUserExtraProperties.GenderPropertyTitle}='M'  or {AbpUserExtraProperties.GenderPropertyTitle} = 'F' ");

            })

            ;
        });
    }
}
