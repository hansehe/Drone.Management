using System;
using System.IO;
using System.Reflection;
using Drone.Management.Commons;
using Xunit;

namespace Drone.Management.Config.Tests
{
    public class InputResolverTests
    {
        public const string EmbeddedSettingsExampleFile = "SettingsExample.xml";

        public static string[] GetInputArgs(string settingsFilename)
        {
            var args = new[]
            {
                $"settings={settingsFilename}"
            };
            return args;
        }

        public static string[] GetEmptyInputArgs()
        {
            return new string[0];
        }

        [Fact]
        public void SetSettings_FullInput_Success()
        {
            var settingsFilename = SaveLocalSettingsExampleFile(EmbeddedSettingsExampleFile);
            var args = GetInputArgs(settingsFilename);
            InputResolver.SetSettingsFromInput(args);
            DeleteLocalSettingsExampleFile(settingsFilename);
        }

        [Fact]
        public void SetSettings_EmptyInput_Success()
        {
            var args = GetEmptyInputArgs();
            InputResolver.SetSettingsFromInput(args);
        }

        private static string SaveLocalSettingsExampleFile(string EmbeddedExampleFile)
        {
            var content = EmbeddedResourceHandler.GetEmbeddedResource($"{typeof(InputResolverTests).Namespace}.{EmbeddedExampleFile}",
                Assembly.GetAssembly(typeof(InputResolverTests)));
            var filename = Path.GetTempFileName();
            File.WriteAllText(filename, content);
            return filename;
        }

        private static void DeleteLocalSettingsExampleFile(string filename)
        {
            try
            {
                File.Delete(filename);
            }
            catch (Exception e)
            {
            }
        }
    }
}
