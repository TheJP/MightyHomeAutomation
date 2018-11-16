using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MightyHomeAutomation.Logic.Devices
{
    public abstract class DeviceTypeBase : DeviceType
    {
        private readonly IDictionary<string, Action<IDictionary<string, string>>> actions = new Dictionary<string, Action<IDictionary<string, string>>>();
        private readonly IDictionary<string, Func<IList<string>, string>> sensors = new Dictionary<string, Func<IList<string>, string>>();

        public IEnumerable<string> Actions => actions.Keys;

        public IEnumerable<string> Sensors => sensors.Keys;

        public bool ContainsAction(string name) => actions.ContainsKey(name);

        public bool ContainsSensor(string name) => sensors.ContainsKey(name);

        public void ExecuteAction(string name, IDictionary<string, string> parameter = null) => actions[name](parameter);

        public string ReadSensor(string name, IList<string> parameter = null) => sensors[name](parameter);

        protected void AddAction(string id, Action<IDictionary<string, string>> action) => actions.Add(id, action);

        protected void AddSensor(string id, Func<IList<string>, string> sensor) => sensors.Add(id, sensor);
    }
}