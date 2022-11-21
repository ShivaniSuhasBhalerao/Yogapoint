using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TestLoginYogapoint;

[Dependency(ReplaceServices = true)]
public class TestLoginYogapointBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TestLoginYogapoint";
}
