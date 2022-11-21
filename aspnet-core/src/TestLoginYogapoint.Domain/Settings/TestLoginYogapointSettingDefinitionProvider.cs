using Volo.Abp.Settings;

namespace TestLoginYogapoint.Settings;

public class TestLoginYogapointSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TestLoginYogapointSettings.MySetting1));
    }
}
