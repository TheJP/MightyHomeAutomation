using Newtonsoft.Json;
using System.Collections.Generic;

namespace MightyHomeAutomation.Persistence
{
    public class Configuration
    {
        [JsonProperty("devices")]
        public IList<Device> Devices { get; set; }

        [JsonProperty("viewCards")]
        public IList<ViewCard> ViewCards { get; set; }
    }
}