using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class ViewComponent
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("options")]
        public string Options { get; }

        [JsonConstructor]
        public ViewComponent(string type, string options){
            Type = type;
            Options = options;
        }
    }
}