using System.Reflection;
using Drone.Management.Repository.Commons;
using Drone.Management.Repository.Commons.RepositoryValues;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.PostgreSql
{
    public class PostgreSqlDroneRepository : DroneRepository<PostgreSqlSettings>
    {
        protected override AvailableDbConnections DbConnectionType { get; } = AvailableDbConnections.NpgsqlConnection;

        protected override string SqlCommandsResourcePath { get; } =
            $"{typeof(PostgreSqlDroneRepository).Namespace}.SqlCommands.DroneSqlCommands.json";

        public PostgreSqlDroneRepository(ISqlRepository repository, IDapperInitializer dapperInitializer) : base(repository, dapperInitializer, Assembly.GetAssembly(typeof(PostgreSqlDroneRepository)))
        {
        }
    }
}
