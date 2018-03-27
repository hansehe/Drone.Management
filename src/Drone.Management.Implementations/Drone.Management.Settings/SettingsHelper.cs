using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Drone.Management.Settings
{
    internal static class SettingsHelper
    {
        internal static XmlSchemaSet PrepareValidationSchema(IList<string> xmlValidationSchemas)
        {
            var validationSchemaSet = new XmlSchemaSet();

            foreach (var xmlValidationSchema in xmlValidationSchemas)
            {
                validationSchemaSet.Add(XmlSchema.Read(new StringReader(xmlValidationSchema), null));
            }

            validationSchemaSet.Compile();

            return validationSchemaSet;
        }

        internal static T DeserializeSettings<T>(XDocument settings)
        {
            var serializer = new XmlSerializer(typeof(T));
            Stream xmlStream = new MemoryStream();
            settings.Save(xmlStream);
            xmlStream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(xmlStream);
        }

        internal static XDocument SerializeSettings<T>(T model)
        {
            var serializer = new XmlSerializer(typeof(T));
            var xmlStream = new MemoryStream();
            serializer.Serialize(xmlStream, model);
            xmlStream.Seek(0, SeekOrigin.Begin);
            return XDocument.Load(xmlStream);
        }
    }
}
