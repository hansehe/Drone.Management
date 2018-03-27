using System.Reflection;
using Newtonsoft.Json;

namespace Drone.Management.Commons
{
    public static class JsonParser
    {
        public static T GetDeserializedObjectFromResource<T>(string sqlResourcePath, Assembly callingAssembly = null)
        {
            callingAssembly = callingAssembly ?? Assembly.GetCallingAssembly();
            var sqlCommandsJson = EmbeddedResourceHandler.GetEmbeddedResource(sqlResourcePath, callingAssembly);
            return DeserializeJson<T>(sqlCommandsJson);
        }

        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string SerializeObject<T>(T @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}
