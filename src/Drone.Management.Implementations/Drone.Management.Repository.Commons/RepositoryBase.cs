using System;
using System.Reflection;
using Drone.Management.Commons;
using Drone.Management.Repository.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Repository.Commons
{
    public abstract class RepositoryBase<T> : IRepositoryResolver
    {
        private readonly IRepositoryCommands RepositoriesCommandsField;

        private T Settings { get; set; }

        private Type SettingsType { get; } = typeof(T);

        protected abstract AvailableDbConnections DbConnectionType { get; }

        protected abstract string SqlCommandsResourcePath { get; }

        protected readonly ISqlRepository RepositoryField;

        private readonly IDapperInitializer DapperInitializerField;

        protected RepositoryBase(ISqlRepository repository, IDapperInitializer dapperInitializer, Assembly callingAssembly)
        {
            RepositoryField = repository;
            DapperInitializerField = dapperInitializer;
            RepositoriesCommandsField = RepositoryTools.LoadAndAssertRepositorySqlCommands(SqlCommandsResourcePath, callingAssembly);
        }

        public bool CanUseRepository(ISettings settings)
        {
            return settings.Settings.SettingsModel.Item.GetType() == SettingsType;
        }

        public void SetupRepository(ISettings settings)
        {
            Settings = (T)settings.Settings.SettingsModel.Item;
            BasicSqlCommandsGenerator.UpdateBasicSqlCommands(RepositoriesCommandsField, settings);
            DapperInitializerField.InitializeDapper();
            RepositoryField.SetupRepository(DbConnectionType, RepositoriesCommandsField);
        }
    }
}
