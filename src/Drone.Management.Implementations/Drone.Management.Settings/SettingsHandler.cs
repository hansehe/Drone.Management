using System;
using System.Xml.Linq;
using System.Xml.Schema;
using Drone.Management.Config;
using Drone.Management.Settings.Interfaces;

namespace Drone.Management.Settings
{
    public class SettingsHandler : ISettings
    {
        public Interfaces.Settings Settings { get; set; }

        private readonly XmlSchemaSet SettingsValidationSchemaField;

        public SettingsHandler()
        {
            SettingsValidationSchemaField = GetValidationSchemaSet();
            DeserializeSettings(SettingsResolver.Settings);
        }

        public bool ValidateSettings(XDocument settings)
        {
            try
            {
                settings.Validate(SettingsValidationSchemaField, null);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeserializeSettings(XDocument settings)
        {
            if (!ValidateSettings(settings))
            {
                throw new Exception(
                    "Invalid migration setting schema. Please follow the RepositorySettingsSchema.xsd schema.");
            }
            Settings = SettingsHelper.DeserializeSettings<Interfaces.Settings>(settings);
        }

        public XDocument SerializeSettings()
        {
            return SettingsHelper.SerializeSettings(Settings);
        }

        private XmlSchemaSet GetValidationSchemaSet()
        {
            return SettingsHelper.PrepareValidationSchema(EmbeddedSettingsSchemas.GetEmbeddedSettingsSchemas());
        }
    }
}
