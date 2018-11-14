using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MightyHomeAutomation.Logic.Devices
{
    public class DeviceManager
    {
        private readonly Dictionary<string, DeviceType> devices = new Dictionary<string, DeviceType>();
        public IReadOnlyDictionary<string, DeviceType> Devices =>
            new ReadOnlyDictionary<string, DeviceType>(devices);

        public DeviceManager()
        {
            var list = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Select(type =>
                {
                    var attributes = type.GetCustomAttributes(typeof(RegisterDeviceTypeAttribute), false);
                    var attribute = attributes == null || attributes.Length <= 0 ? null : attributes[0];
                    return (type, attribute);
                })
                .Where(tuple => tuple.attribute != null)
                .ToList();

            // TODO: Add devices to the dictionary
            foreach (var e in list)
            {
                Console.WriteLine(e.attribute);
            }
        }
    }
}