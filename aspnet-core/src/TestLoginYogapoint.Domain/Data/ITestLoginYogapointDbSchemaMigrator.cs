using System.Threading.Tasks;

namespace TestLoginYogapoint.Data;

public interface ITestLoginYogapointDbSchemaMigrator
{
    Task MigrateAsync();
}
