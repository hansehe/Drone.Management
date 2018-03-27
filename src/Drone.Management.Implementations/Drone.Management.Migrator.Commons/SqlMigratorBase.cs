using System;
using System.Reflection;
using System.Threading.Tasks;
using Drone.Management.Commons;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator.Commons
{
    public abstract class SqlMigratorBase<T> : IMigratorResolver
    {
        private readonly IMigratorTool MigratorToolField;

        private IMigratorSqlCommands MigratorSqlCommandsField;

        private T SettingsField { get; set; }

        protected abstract AvailableMigratorTools MigratorTool { get; }

        protected abstract string SqlCommandsResourcePath { get; }

        protected abstract Assembly CallingAssembly { get; }

        protected SqlMigratorBase(IMigratorToolResolver migratorToolResolver)
        {
            MigratorToolField = migratorToolResolver.ResolveMigratorTool(MigratorTool);
            MigratorSqlCommandsField = SetAndAssertMigratorSqlCommands(MigratorSqlCommandsField);
        }

        public bool CanMigrate(ISettings settings)
        {
            return settings.Settings.SettingsModel.Item.GetType() == typeof(T);
        }

        public async Task SetupMigration(ISettings settings)
        {
            if (!CanMigrate(settings))
            {
                throw new ArgumentException("Invalid settings input - Settings must contain correct settings type model");
            }
            SettingsField = (T)settings.Settings.SettingsModel.Item;
            BasicSqlCommandsGenerator.UpdateBasicSqlCommands(MigratorSqlCommandsField, settings);
        }

        public Task Migrate()
        {
            return MigratorToolField.Migrate(MigratorSqlCommandsField, CallingAssembly);
        }

        public Task Connect()
        {
            return MigratorToolField.Connect(MigratorSqlCommandsField, CallingAssembly);
        }

        private IMigratorSqlCommands SetAndAssertMigratorSqlCommands(IMigratorSqlCommands migratorSqlCommands)
        {
            migratorSqlCommands = JsonParser.GetDeserializedObjectFromResource<MigratorSqlCommands>(SqlCommandsResourcePath, CallingAssembly);
            MigratorSqlCommands.AssertMigratorSqlCommandsAreSet(migratorSqlCommands);
            return migratorSqlCommands;
        }
    }
}
