using Drone.Management.Repository.Commons.TypeValues;
using Microsoft.Extensions.DependencyInjection;

namespace Drone.Management.Repository.Commons
{
    public static class ServiceCollectionExtension
    {
        public static void AddCommonRepositoryServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnectionWrapper, DbConnectionWrapper>();
            services.AddScoped<ISqlRepository, SqlRepository>();
            services.AddSingleton<IDapperInitializer, DapperInitializer>();
            services.AddScoped<ITypeHandler, IdentityHandler>();
        }
    }
}
