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
        public IReadOnlyList<string> Parameters { get; }

        public Device(string id, string type, IReadOnlyList<string> parameters)
        {
            Id = id;
            Type = type;
            Parameters = parameters;
        }
    }
}