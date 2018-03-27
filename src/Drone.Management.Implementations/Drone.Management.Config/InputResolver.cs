using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace Drone.Management.Config
{
    public static class InputResolver
    {
        public static void SetSettingsFromInput(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            if (!TrySetSettingsFromEnvironment())
            {
                TrySetSettingsFromConfiguration(config.Providers);
            }
        }

        private static bool TrySetSettingsFromEnvironment()
        {
            string settingsPath;
            if ((settingsPath = Environment.GetEnvironmentVariable("SETTINGS")) == null)
            {
                return false;
            }
            SetSettingsFromFile(settingsPath);
            return true;
        }

        private static void TrySetSettingsFromConfiguration(IEnumerable<IConfigurationProvider> configs)
        {
            if (!configs.Any())
            {
                return;
            }
            if (configs.First().TryGet("settings", out var settingsPath))
            {
                SetSettingsFromFile(settingsPath);
            }
        }

        private static void SetSettingsFromFile(string settingsPath)
        {
            if (!File.Exists(settingsPath))
            {
                Console.WriteLine($"Could not find settings file: {settingsPath}");
                return;
            }
            var rawSettings = File.ReadAllText(settingsPath);
            SettingsResolver.Settings = XDocument.Parse(rawSettings);
        }
    }
}
