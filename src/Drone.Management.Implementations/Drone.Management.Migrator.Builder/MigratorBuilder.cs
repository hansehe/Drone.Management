using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator.Builder
{
    public class MigratorBuilder : IMigratorBuilder
    {
        private readonly IEnumerable<IMigratorResolver> MigratorResolversField;

        public MigratorBuilder(IEnumerable<IMigratorResolver> migratorResolvers)
        {
            MigratorResolversField = migratorResolvers;
        }

        public async Task<IMigrator> BuildMigrator(ISettings settings)
        {
            foreach (var migratorResolver in MigratorResolversField)
            {
                if (migratorResolver.CanMigrate(settings))
                {
                    await migratorResolver.SetupMigration(settings);
                    return migratorResolver;
                }
            }

            throw new Exception("Cannot resolve migrator!");
        }
    }
}
