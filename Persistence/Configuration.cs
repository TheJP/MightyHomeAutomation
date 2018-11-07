using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class Configuration
    {
        [JsonRequired]
        [JsonProperty("devices")]
        public IList<Device> Devices { get; }

        [JsonRequired]
        [JsonProperty("viewCards")]
        public IList<ViewCard> ViewCards { get; }

        [JsonConstructor]
        public Configuration(IList<Device> devices, IList<ViewCard> viewCards)
        {
            Devices = devices;
            ViewCards = viewCards;
        }
    }
}