using System.IO;
using System.Reflection;

namespace Drone.Management.Commons
{
    public static class EmbeddedResourceHandler
    {
        public static string GetEmbeddedResource(string resourcePath, Assembly callingAssembly = null)
        {
            callingAssembly = callingAssembly ?? Assembly.GetCallingAssembly();
            var objStream = callingAssembly.GetManifestResourceStream(resourcePath);
            var objReader = new StreamReader(objStream);
            return objReader.ReadToEnd();
        }
    }
}
