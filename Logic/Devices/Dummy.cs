using System;
using System.Collections.Generic;
using System.Threading;

namespace MightyHomeAutomation.Logic.Devices
{
    [RegisterDeviceType("DummyDevice")]
    public class Dummy : DeviceTypeBase
    {
        private int value = 25;

        public Dummy(){
            AddSensor("Temperature", _ => value.ToString());
            AddAction("IncreaseTemperature", _ => Interlocked.Increment(ref value));
        }
    }
}