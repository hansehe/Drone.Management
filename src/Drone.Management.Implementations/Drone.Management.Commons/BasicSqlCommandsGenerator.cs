using Drone.Management.Commons.Interfaces;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Commons
{
    public static class BasicSqlCommandsGenerator
    {
        public static void UpdateBasicSqlCommands(IBasicSqlCommands basicSqlCommands, ISettings settings)
        {
            var settingsModel = settings.Settings.SettingsModel.Item;
            var settingsType = settingsModel.GetType();
            if (settingsType == typeof(PostgreSqlSettings))
            {
                basicSqlCommands.Connect = GenerateSqlConnect((PostgreSqlSettings) settingsModel);
            }
        }

        public static string GenerateSqlConnect(PostgreSqlSettings settings)
        {
            var sqlConnect =
                $"User ID={settings.SqlConnect.UserId};Password={settings.SqlConnect.Password};Host={settings.SqlConnect.Host};Port={settings.SqlConnect.Port};Database={settings.SqlConnect.DatabaseName};Pooling={settings.SqlConnect.Pooling};";
            return sqlConnect;
        }
    }
}
