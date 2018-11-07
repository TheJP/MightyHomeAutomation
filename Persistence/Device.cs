using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class Device
    {
        /// <summary>
        /// Display name that is used to represent the device to the client.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("parameters")]
        public string Parameters { get; }

        public Device(string name, string type, string parameters)
        {
            Name = name;
            Type = type;
            Parameters = parameters;
        }
    }
}