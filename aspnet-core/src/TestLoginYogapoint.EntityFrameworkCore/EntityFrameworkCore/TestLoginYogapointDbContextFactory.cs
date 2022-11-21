using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TestLoginYogapoint.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TestLoginYogapointDbContextFactory : IDesignTimeDbContextFactory<TestLoginYogapointDbContext>
{
    public TestLoginYogapointDbContext CreateDbContext(string[] args)
    {
        TestLoginYogapointEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TestLoginYogapointDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TestLoginYogapointDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TestLoginYogapoint.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
