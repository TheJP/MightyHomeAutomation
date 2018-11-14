using System;
using System.Collections.Generic;

namespace MightyHomeAutomation.Logic.Devices
{
    [RegisterDeviceType("DummyDevice")]
    public class Dummy : DeviceTypeBase
    {
        public Dummy() : base(
            new Dictionary<string, Action<string>>(),
            new Dictionary<string, Func<string, string>>())
        {
        }
    }
}