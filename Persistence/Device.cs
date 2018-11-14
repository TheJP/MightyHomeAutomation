using System.Collections.Generic;
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
        public IReadOnlyDictionary<string, string> Parameters { get; }

        public Device(string id, string type, IReadOnlyDictionary<string, string> parameters)
        {
            Id = id;
            Type = type;
            Parameters = parameters;
        }
    }
}