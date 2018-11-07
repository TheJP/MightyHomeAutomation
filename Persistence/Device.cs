using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class Device
    {
        /// <summary>
        /// Display name that is used to represent the device to the client.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}