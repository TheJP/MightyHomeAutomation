using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MightyHomeAutomation.Logic.Devices
{
    public class DeviceTypeManager
    {
        private readonly Dictionary<string, DeviceType> devices;
        public IReadOnlyDictionary<string, DeviceType> Devices =>
            new ReadOnlyDictionary<string, DeviceType>(devices);

        private DeviceTypeManager(Dictionary<string, DeviceType> devices) => this.devices = devices;

        public static DeviceTypeManager Load() => new DeviceTypeManager(AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Select(type =>
            {
                var attributes = type.GetCustomAttributes(typeof(RegisterDeviceTypeAttribute), false);
                var attribute = (attributes == null || attributes.Length <= 0 ? null : attributes[0]) as RegisterDeviceTypeAttribute;
                return (type, attribute);
            })
            .Where(tuple => tuple.attribute != null)
            .Select(tuple => (deviceType: Activator.CreateInstance(tuple.type) as DeviceType, tuple.attribute))
            .Where(tuple => tuple.deviceType != null)
            .ToDictionary(tuple => tuple.attribute.DeviceTypeId, tuple => tuple.deviceType));
    }
}