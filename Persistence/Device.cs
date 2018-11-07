using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class Device
    {
        [JsonRequired]
        [JsonProperty("id")]
        public string Id { get; }

        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("parameters")]
        public string Parameters { get; }

        public Device(string id, string type, string parameters)
        {
            Id = id;
            Type = type;
            Parameters = parameters;
        }
    }
}