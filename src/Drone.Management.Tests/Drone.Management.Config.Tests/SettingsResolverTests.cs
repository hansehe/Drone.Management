using Xunit;

namespace Drone.Management.Config.Tests
{
    public class SettingsResolverTests
    {
        [Fact]
        public void GetDefaultSqlSettings_Success()
        {
            var defaultSettings = SettingsResolver.GetDefaultSqlSettings();
        }

        [Fact]
        public void GetPostgreSqlSettings_Success()
        {
            var settings = SettingsResolver.GetDefaultPostgreSqlSettings();
        }
    }
}
