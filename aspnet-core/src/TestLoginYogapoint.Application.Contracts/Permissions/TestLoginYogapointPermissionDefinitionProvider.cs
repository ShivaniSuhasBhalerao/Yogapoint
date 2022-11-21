using TestLoginYogapoint.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestLoginYogapoint.Permissions;

public class TestLoginYogapointPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestLoginYogapointPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TestLoginYogapointPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestLoginYogapointResource>(name);
    }
}
