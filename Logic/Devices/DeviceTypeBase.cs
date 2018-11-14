using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MightyHomeAutomation.Logic.Devices
{
    public abstract class DeviceTypeBase : DeviceType
    {
        private readonly IDictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();
        private readonly IDictionary<string, Func<string, string>> sensors = new Dictionary<string, Func<string, string>>();

        public IReadOnlyDictionary<string, Action<string>> Actions => new ReadOnlyDictionary<string, Action<string>>(actions);

        public IReadOnlyDictionary<string, Func<string, string>> Sensors => new ReadOnlyDictionary<string, Func<string, string>>(sensors);

        protected void AddAction(string id, Action<string> action) => actions.Add(id, action);

        protected void AddSensor(string id, Func<string, string> sensor) => sensors.Add(id, sensor);
    }
}