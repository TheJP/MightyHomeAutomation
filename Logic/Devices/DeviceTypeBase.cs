using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MightyHomeAutomation.Logic.Devices
{
    public abstract class DeviceTypeBase : DeviceType
    {
        private readonly IDictionary<string, Action<string>> actions = new Dictionary<string, Action<string>>();
        private readonly IDictionary<string, Func<string, string>> sensors = new Dictionary<string, Func<string, string>>();

        public IEnumerable<string> Actions => actions.Keys;

        public IEnumerable<string> Sensors => sensors.Keys;

        public bool ContainsAction(string name) => actions.ContainsKey(name);

        public bool ContainsSensor(string name) => sensors.ContainsKey(name);

        public void ExecuteAction(string name, string parameter = "") => actions[name](parameter);

        public string ReadSensor(string name, string parameter = "") => sensors[name](parameter);

        protected void AddAction(string id, Action<string> action) => actions.Add(id, action);

        protected void AddSensor(string id, Func<string, string> sensor) => sensors.Add(id, sensor);
    }
}