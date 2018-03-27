using System;
using System.Threading.Tasks;
using Drone.Management.Migrator.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Migrator
{
    internal static class SafeMigration
    {
        private const int MaxConnectionTries = 3;
        private const int MsSleepBetweenTries = 500;

        internal static async Task ExecuteMigration(IMigratorBuilder migratorBuilder, ISettings settings, int connectTries = 0, int maxConnectionTries = MaxConnectionTries)
        {
            var migrator = await migratorBuilder.BuildMigrator(settings);
            try
            {
                await migrator.Connect();
            }
            catch (Exception e)
            {
                if (connectTries >= maxConnectionTries)
                {
                    throw;
                }
                connectTries++;
                System.Threading.Thread.Sleep(MsSleepBetweenTries);
                await ExecuteMigration(migratorBuilder, settings, connectTries, maxConnectionTries);
                return;
            }
            await migrator.Migrate();
        }
    }
}
