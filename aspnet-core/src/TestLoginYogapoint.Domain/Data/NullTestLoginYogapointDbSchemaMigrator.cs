using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TestLoginYogapoint.Data;

/* This is used if database provider does't define
 * ITestLoginYogapointDbSchemaMigrator implementation.
 */
public class NullTestLoginYogapointDbSchemaMigrator : ITestLoginYogapointDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
