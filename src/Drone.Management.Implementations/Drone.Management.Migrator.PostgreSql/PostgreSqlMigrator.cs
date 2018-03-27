using System.Reflection;
using Drone.Management.Migrator.Commons;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator.PostgreSql
{
    public class PostgreSqlMigrator : SqlMigratorBase<PostgreSqlSettings>
    {
        protected override AvailableMigratorTools MigratorTool { get; } = AvailableMigratorTools.DbUpPostgreSql;

        protected override string SqlCommandsResourcePath { get; } = $"{typeof(PostgreSqlMigrator).Namespace}.SqlCommands.json";

        protected override Assembly CallingAssembly { get; } = Assembly.GetAssembly(typeof(PostgreSqlMigrator));

        public PostgreSqlMigrator(IMigratorToolResolver migratorToolResolver) : base(migratorToolResolver)
        {
        }
    }
}
