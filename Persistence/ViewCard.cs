using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class ViewCard
    {
        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("components")]
        public IReadOnlyList<ViewElement> Components { get; }

        [JsonConstructor]
        public ViewCard(string title, IReadOnlyList<ViewElement> components)
        {
            Title = title;
            Components = components;
        }
    }
}