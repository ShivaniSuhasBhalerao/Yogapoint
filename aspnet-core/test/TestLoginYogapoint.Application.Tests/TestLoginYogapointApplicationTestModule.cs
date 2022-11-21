using Volo.Abp.Modularity;

namespace TestLoginYogapoint;

[DependsOn(
    typeof(TestLoginYogapointApplicationModule),
    typeof(TestLoginYogapointDomainTestModule)
    )]
public class TestLoginYogapointApplicationTestModule : AbpModule
{

}
