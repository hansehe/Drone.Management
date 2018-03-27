using System;
using System.Reflection;
using System.Threading.Tasks;
using DbUp;
using DbUp.Engine;

namespace Drone.Management.Migrator.Commons.MigratorTools
{
    public abstract class DbUpMigratorBase : IMigratorTool
    {
        private UpgradeEngine UpgradeEngineField;

        private bool SetupMigrationFinished { get; set; } = false;

        protected abstract AvailableMigratorTools MigratorTool { get; }

        protected abstract UpgradeEngine BuildUpgradeEngine(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly);

        protected abstract void EnsureValidDatabase(IMigratorSqlCommands migratorSqlCommands);

        public bool CanMigrate(AvailableMigratorTools migratorTool)
        {
            return migratorTool == MigratorTool;
        }

        public async Task Migrate(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly)
        {
            SetupMigration(migratorSqlCommands, callingAssembly);
            var result = UpgradeEngineField.PerformUpgrade();
            if (!result.Successful)
            {
                throw new Exception("Database migration upgrade failed!");
            }
        }

        public async Task Connect(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly)
        {
            SetupMigration(migratorSqlCommands, callingAssembly);
        }

        private void SetupMigration(IMigratorSqlCommands migratorSqlCommands, Assembly callingAssembly)
        {
            if (SetupMigrationFinished)
            {
                return;
            }
            UpgradeEngineField = BuildUpgradeEngine(migratorSqlCommands, callingAssembly);
            EnsureValidDatabase(migratorSqlCommands);
            if (!UpgradeEngineField.TryConnect(out var errorMessage))
            {
                throw new Exception(errorMessage);
            }
            SetupMigrationFinished = true;
        }
    }
}
