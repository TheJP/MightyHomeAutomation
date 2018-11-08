using System.Collections.Generic;
using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class ViewCard
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("parameters")]
        public IReadOnlyList<string> Parameters { get; }

        [JsonConstructor]
        public ViewCard(string type, IReadOnlyList<string> parameters)
        {
            Type = type;
            Parameters = parameters;
        }
    }
}