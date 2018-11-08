using System;
using System.Collections.Generic;

namespace MightyHomeAutomation.Logic.Devices
{
    public interface DeviceType
    {
        IReadOnlyDictionary<string, Action<string>> Actions { get; }
        IReadOnlyDictionary<string, Func<string, string>> Sensors { get; }
    }
}