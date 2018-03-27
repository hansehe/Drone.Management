using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace Drone.Management.Migrator.Commons.MigratorTools
{
    public class DbUpPostgreSqlMigrator : DbUpMigratorBase
    {
        protected override AvailableMigratorTools MigratorTool { get; } = AvailableMigratorTools.DbUpPostgreSql;

        protected override UpgradeEngine BuildUpgradeEngine(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly)
        {
            var upgradeEngine = DeployChanges.To
                .PostgresqlDatabase(migratorSqlCommands.Connect)
                .WithScripts()
                .WithScriptsEmbeddedInAssembly(callingAssembly)
                .LogToConsole()
                .Build();
            return upgradeEngine;
        }

        protected override void EnsureValidDatabase(IMigratorSqlCommands migratorSqlCommands)
        {
            EnsureDatabase.For.PostgresqlDatabase(migratorSqlCommands.Connect);
        }
    }
}
