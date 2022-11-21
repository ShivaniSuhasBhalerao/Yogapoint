using TestLoginYogapoint.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace TestLoginYogapoint.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestLoginYogapointEntityFrameworkCoreModule),
    typeof(TestLoginYogapointApplicationContractsModule)
    )]
public class TestLoginYogapointDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
