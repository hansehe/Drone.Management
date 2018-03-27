using System;

namespace Drone.Management.Migrator.Commons
{
    public class MigratorSqlCommands : IMigratorSqlCommands
    {
        public string Connect { get; set; }

        public static void AssertMigratorSqlCommandsAreSet(IMigratorSqlCommands migratorSqlCommands)
        {
            AssertObjectIsNotNull(migratorSqlCommands.Connect, "migratorSqlCommands.Connect is null");
        }

        private static void AssertObjectIsNotNull(object @object, string errorMsg)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(errorMsg);
            }
        }
    }
}
