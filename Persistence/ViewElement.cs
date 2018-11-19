using Newtonsoft.Json;

namespace MightyHomeAutomation.Persistence
{
    public class ViewElement
    {
        [JsonRequired]
        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("options")]
        public string Options { get; }

        [JsonConstructor]
        public ViewElement(string type, string options){
            Type = type;
            Options = options;
        }
    }
}