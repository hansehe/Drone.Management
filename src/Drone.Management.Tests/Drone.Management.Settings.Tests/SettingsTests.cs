using System;
using System.Xml.Linq;
using Drone.Management.Commons;
using Drone.Management.Config;
using Drone.Management.Settings.Interfaces;
using FluentAssertions;
using Xunit;

namespace Drone.Management.Settings.Tests
{
    public class SettingsTests
    {
        public static ISettings GetSettings()
        {
            return new SettingsHandler();
        }

        public static XDocument GetValidXmlSettings()
        {
            return SettingsResolver.GetDefaultSqlSettings();
        }

        public static XDocument GetInvalidXmlSettings()
        {
            return XDocument.Parse(EmbeddedResourceHandler.GetEmbeddedResource($"{typeof(SettingsTests).Namespace}.Resources.InvalidSettings.xml"));
        }

        [Fact]
        public void GenerateSettings_Success()
        {
            var settings = GetSettings();
            var xmlSettings = settings.SerializeSettings();
            settings.ValidateSettings(xmlSettings).Should().BeTrue();
        }

        [Fact]
        public void ValidateSettings_IsTrue()
        {
            var settings = GetSettings();
            var xmlSettings = GetValidXmlSettings();
            settings.ValidateSettings(xmlSettings).Should().BeTrue();
        }

        [Fact]
        public void ValidateSettings_IsFalse()
        {
            var settings = GetSettings();
            var xmlSettings = GetInvalidXmlSettings();
            settings.ValidateSettings(xmlSettings).Should().BeFalse();
        }

        [Fact]
        public void DeserializeSettings_Success()
        {
            var settings = GetSettings();
            var xmlSettings = GetValidXmlSettings();
            settings.DeserializeSettings(xmlSettings);
        }

        [Fact]
        public void DeserializeSettings_NoSuccess()
        {
            var settings = GetSettings();
            var xmlSettings = GetInvalidXmlSettings();
            Assert.Throws<Exception>(() => settings.DeserializeSettings(xmlSettings));
        }

        [Fact]
        public void SerializeSettings_Success()
        {
            var settings = GetSettings();
            var xmlSettings = settings.SerializeSettings();
            settings.ValidateSettings(xmlSettings).Should().BeTrue();
        }
    }
}
