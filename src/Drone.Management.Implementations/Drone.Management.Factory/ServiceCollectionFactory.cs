using Drone.Management.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Factory
{
    public static class ServiceCollectionFactory
    {
        public static void AddServiceCollection(IServiceCollection services)
        {
            ServiceCollectionExtension.AddSettingsServices(services);
            Business.ServiceCollectionExtension.AddBusinessServices(services);
            Repository.Commons.ServiceCollectionExtension.AddCommonRepositoryServices(services);
            Repository.Builder.ServiceCollectionExtension.AddRepositoryBuilderServices(services);
            Repository.PostgreSql.ServiceCollectionExtension.AddPostgeSqlRepositoryServices(services);
        }

        public static void AddMigratorServiceCollection(IServiceCollection services)
        {
            AddServiceCollection(services);
            Migrator.Commons.ServiceCollectionExtension.AddCommonMigratorServices(services);
            Migrator.Builder.ServiceCollectionExtension.AddMigratorBuilderServices(services);
            Migrator.PostgreSql.ServiceCollectionExtension.AddPostgeSqlMigratorServices(services);
        }
    }
}
