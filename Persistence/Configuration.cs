using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class Configuration
    {
        [JsonProperty("devices")]
        public IList<Device> Devices { get; }

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