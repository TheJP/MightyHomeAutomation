using System;
using System.Collections.Generic;

namespace MightyHomeAutomation.Logic.Devices
{
    public interface DeviceType
    {
        IEnumerable<string> Actions { get; }
        IEnumerable<string> Sensors { get; }

        bool ContainsAction(string name);

        bool ContainsSensor(string name);

        void ExecuteAction(string name, IReadOnlyDictionary<string, string> parameter = null);

        string ReadSensor(string name, IReadOnlyDictionary<string, string> parameter = null);
    }
}