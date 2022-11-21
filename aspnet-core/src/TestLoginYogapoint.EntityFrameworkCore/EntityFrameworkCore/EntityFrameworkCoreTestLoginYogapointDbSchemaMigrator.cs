using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestLoginYogapoint.Data;
using Volo.Abp.DependencyInjection;

namespace TestLoginYogapoint.EntityFrameworkCore;

public class EntityFrameworkCoreTestLoginYogapointDbSchemaMigrator
    : ITestLoginYogapointDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTestLoginYogapointDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TestLoginYogapointDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TestLoginYogapointDbContext>()
            .Database
            .MigrateAsync();
    }
}
