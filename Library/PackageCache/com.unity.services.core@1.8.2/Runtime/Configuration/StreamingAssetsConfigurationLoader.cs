using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration
{
    class StreamingAssetsConfigurationLoader : IConfigurationLoader
    {
        public async Task<SerializableProjectConfiguration> GetConfigAsync()
        {
            var jsonConfig = await StreamingAssetsUtils.GetFileTextFromStreamingAssetsAsync(
                ConfigurationUtils.ConfigFileName);
            using (new JsonConvertDefaultSettingsScope())
            {
                var config = JsonConvert.DeserializeObject<SerializableProjectConfiguration>(jsonConfig);
                return config;
            }
        }
    }
}
