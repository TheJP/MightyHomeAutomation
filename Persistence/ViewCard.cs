using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class ViewCard
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("parameters")]
        public string Parameters { get; }

        [JsonConstructor]
        public ViewCard(string type, string parameters)
        {
            Type = type;
            Parameters = parameters;
        }
    }
}