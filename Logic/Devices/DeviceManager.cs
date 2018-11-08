using System.Collections.Generic;
using System.Collections.ObjectModel;
using MightyHomeAutomation.Persistence;

namespace MightyHomeAutomation.Logic.Devices
{
    public class DeviceManager
    {
        private readonly Dictionary<string, DeviceType> devices = new Dictionary<string, DeviceType>();
        public IReadOnlyDictionary<string, DeviceType> Devices =>
            new ReadOnlyDictionary<string, DeviceType>(devices);

        public DeviceManager(Configuration configuration)
        {
            foreach (var device in configuration.Devices)
            {
                // TODO: Create devices using the type
            }
        }
    }
}