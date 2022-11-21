using TestLoginYogapoint.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TestLoginYogapoint;

[DependsOn(
    typeof(TestLoginYogapointEntityFrameworkCoreTestModule)
    )]
public class TestLoginYogapointDomainTestModule : AbpModule
{

}
