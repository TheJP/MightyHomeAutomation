using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class ViewCard
    {
        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("components")]
        public IReadOnlyList<ViewComponent> Components { get; }

        [JsonConstructor]
        public ViewCard(string title, IReadOnlyList<ViewComponent> components)
        {
            Title = title;
            Components = components;
        }
    }
}