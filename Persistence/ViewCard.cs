using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class ViewCard
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("parameters")]
        public IReadOnlyList<string> Parameters { get; }

        [JsonConstructor]
        public ViewCard(string type, string title, IReadOnlyList<string> parameters)
        {
            Type = type;
            Title = title;
            Parameters = parameters;
        }
    }
}