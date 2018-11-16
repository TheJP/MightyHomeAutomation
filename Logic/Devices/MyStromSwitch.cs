using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MightyHomeAutomation.Logic.Devices
{
    [RegisterDeviceType("MyStromSwitch")]
    public class MyStromSwitch : DeviceTypeBase
    {
        private const string RestApiScheme = "http";

        private enum State { On = 1, Off = 0 }

        public MyStromSwitch()
        {
            AddAction("TurnOn", parameters => ChangeState(parameters["address"], State.On));
            AddAction("TurnOff", parameters => ChangeState(parameters["address"], State.Off));
        }

        private static void ChangeState(string address, State state)
        {
            var uriBuilder = new UriBuilder(RestApiScheme, address);
            var client = new RestClient(uriBuilder.Uri);
            var request = new RestRequest("relay").AddParameter("state", (int) state);
            client.Get(request);
        }
    }
}
