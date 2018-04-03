using System.Reflection;
using Drone.Management.Repository.Commons;
using Drone.Management.Repository.Commons.RepositoryEntities;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.PostgreSql
{
    public class PostgreSqlStatusRepository : StatusRepository<PostgreSqlSettings>
    {
        protected override AvailableDbConnections DbConnectionType { get; } = AvailableDbConnections.NpgsqlConnection;

        protected override string SqlCommandsResourcePath { get; } =
            $"{typeof(PostgreSqlStatusRepository).Namespace}.SqlCommands.StatusSqlCommands.json";

        public PostgreSqlStatusRepository(ISqlRepository repository, IDapperInitializer dapperInitializer) : base(repository, dapperInitializer, Assembly.GetAssembly(typeof(PostgreSqlStatusRepository)))
        {
        }
    }
}
