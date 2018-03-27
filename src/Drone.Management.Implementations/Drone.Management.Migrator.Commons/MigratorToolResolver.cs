using System;
using System.Collections.Generic;

namespace Drone.Management.Migrator.Commons
{
    public class MigratorToolResolver : IMigratorToolResolver
    {
        private readonly IEnumerable<IMigratorTool> MigratorToolsField;

        public MigratorToolResolver(IEnumerable<IMigratorTool> migratorTools)
        {
            MigratorToolsField = migratorTools;
        }

        public IMigratorTool ResolveMigratorTool(AvailableMigratorTools migratorTool)
        {
            foreach (var migrator in MigratorToolsField)
            {
                if (migrator.CanMigrate(migratorTool))
                {
                    return migrator;
                }
            }

            throw new Exception("Cannot resolve migrator tool!");
        }
    }
}
