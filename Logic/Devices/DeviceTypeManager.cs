using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MightyHomeAutomation.Logic.Devices
{
    public class DeviceTypeManager
    {
        private readonly Dictionary<string, DeviceType> devices = new Dictionary<string, DeviceType>();
        public IReadOnlyDictionary<string, DeviceType> Devices =>
            new ReadOnlyDictionary<string, DeviceType>(devices);

        public DeviceTypeManager()
        {
            var deviceTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Select(type =>
                {
                    var attributes = type.GetCustomAttributes(typeof(RegisterDeviceTypeAttribute), false);
                    var attribute = (attributes == null || attributes.Length <= 0 ? null : attributes[0]) as RegisterDeviceTypeAttribute;
                    return (type, attribute);
                })
                .Where(tuple => tuple.attribute != null)
                .Select(tuple => (deviceType: Activator.CreateInstance(tuple.type) as DeviceType, tuple.attribute))
                .Where(tuple => tuple.deviceType != null);

            foreach (var device in deviceTypes)
            {
                devices.Add(device.attribute.DeviceTypeId, device.deviceType);
            }
        }
    }
}