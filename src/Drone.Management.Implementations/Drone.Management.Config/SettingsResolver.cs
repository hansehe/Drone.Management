using System;
using System.Xml.Linq;
using Drone.Management.Commons;

namespace Drone.Management.Config
{
    public static class SettingsResolver
    {
        private static readonly string DefaultPostgreSqlSettings = $"{typeof(SettingsResolver).Namespace}.DefaultSettings.DefaultPostgreSqlSettings.xml";

        public static XDocument Settings { get; set; } = GetDefaultSqlSettings();

        public static XDocument GetDefaultSqlSettings()
        {
            switch (Environment.GetEnvironmentVariable("DEFAULT_SETTINGS"))
            {
                case "POSTGRESQL":
                default:
                    return GetDefaultPostgreSqlSettings();
            }
        }

        public static XDocument GetDefaultPostgreSqlSettings()
        {
            return XDocument.Parse(EmbeddedResourceHandler.GetEmbeddedResource(DefaultPostgreSqlSettings));
        }
    }
}
