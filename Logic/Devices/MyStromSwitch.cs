using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MightyHomeAutomation.Logic.Devices
{
    [RegisterDeviceType("MyStromSwitch")]
    public class MyStromSwitch : DeviceTypeBase
    {
        public MyStromSwitch()
        {
            AddAction("TurnOn", _ => { });
            AddAction("TurnOff", _ => { });
        }
    }
}
