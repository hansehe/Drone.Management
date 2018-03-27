using System.Collections.Generic;
using System.Xml.Linq;
using Drone.Management.Config;
using Drone.Management.Settings.Interfaces;
using Xunit;

namespace Drone.Management.Settings.Tests
{
    public class SettingsModelTests
    {
        public static ISettings GetSettings()
        {
            return new SettingsHandler();
        }

        public static XDocument GetPostgreSqlXmlSettings()
        {
            return SettingsResolver.GetDefaultPostgreSqlSettings();
        }

        public static IEnumerable<object[]> SettingsTestData()
        {
            return new List<object[]>
            {
                new object[] { GetPostgreSqlXmlSettings() }
            };
        }

        [Theory]
        [MemberData(nameof(SettingsTestData))]
        public void DeserializeSettings_Success(XDocument xmlSettings)
        {
            var settings = GetSettings();
            settings.DeserializeSettings(xmlSettings);
        }
    }
}
