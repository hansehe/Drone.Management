using System;
using System.Collections.Generic;
using System.Reflection;
using Drone.Management.Commons;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Settings
{
    public static class EmbeddedSettingsSchemas
    {
        private static readonly Type ResourceAssemblyType = typeof(ISettings);
        private static readonly string SettingsSchema = $"{ResourceAssemblyType.Namespace}.Schemas.SettingsSchema.xsd";
        private static readonly string GeneralSettingsSchema = $"{ResourceAssemblyType.Namespace}.Schemas.Settings.GeneralSettings.xsd";
        private static readonly string PostgreSqlSettingsSchema = $"{ResourceAssemblyType.Namespace}.Schemas.Settings.PostgreSqlSettings.xsd";
        private static readonly string MySqlSettingsSchema = $"{ResourceAssemblyType.Namespace}.Schemas.Settings.MySqlSettings.xsd";

        public static IList<string> GetEmbeddedSettingsSchemas()
        {
            var schemas = new List<string>
            {
                GetSettingsSchema(),
                GetGeneralSettingsSchema(),
                GetPostgreSqlSettingsSchema(),
                GetMySqlSettingsSchema()
            };
            return schemas;
        }

        private static string GetSettingsSchema()
        {
            return EmbeddedResourceHandler.GetEmbeddedResource(SettingsSchema, Assembly.GetAssembly(ResourceAssemblyType));
        }

        private static string GetGeneralSettingsSchema()
        {
            return EmbeddedResourceHandler.GetEmbeddedResource(GeneralSettingsSchema, Assembly.GetAssembly(ResourceAssemblyType));
        }

        private static string GetPostgreSqlSettingsSchema()
        {
            return EmbeddedResourceHandler.GetEmbeddedResource(PostgreSqlSettingsSchema, Assembly.GetAssembly(ResourceAssemblyType));
        }

        private static string GetMySqlSettingsSchema()
        {
            return EmbeddedResourceHandler.GetEmbeddedResource(MySqlSettingsSchema, Assembly.GetAssembly(ResourceAssemblyType));
        }
    }
}
