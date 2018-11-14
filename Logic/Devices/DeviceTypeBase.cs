using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MightyHomeAutomation.Logic.Devices
{
    public abstract class DeviceTypeBase : DeviceType
    {
        private readonly IDictionary<string, Action<string>> actions;
        private readonly IDictionary<string, Func<string, string>> sensors;

        public IReadOnlyDictionary<string, Action<string>> Actions => new ReadOnlyDictionary<string, Action<string>>(actions);

        public IReadOnlyDictionary<string, Func<string, string>> Sensors => new ReadOnlyDictionary<string, Func<string, string>>(sensors);

        public DeviceTypeBase(IDictionary<string, Action<string>> actions, IDictionary<string, Func<string, string>> sensors)
        {
            this.actions = actions;
            this.sensors = sensors;
        }
    }
}