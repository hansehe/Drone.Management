using System.Xml.Linq;

namespace Drone.Management.Settings.Interfaces
{
    public interface ISettings
    {
        Settings Settings { get; set; }

        bool ValidateSettings(XDocument settings);

        void DeserializeSettings(XDocument settings);

        XDocument SerializeSettings();
    }
}
