using Newtonsoft.Json;
using RestSharp;
using System;

namespace MightyHomeAutomation.Logic.Devices
{
    [RegisterDeviceType("MyStromSwitch")]
    public class MyStromSwitch : DeviceTypeBase
    {
        private const string RestApiScheme = "http";
        private const string ParametersAddressKey = "address";

        private enum State { On = 1, Off = 0 }

        private class StateConverter : JsonConverter<State>
        {
            public override State ReadJson(JsonReader reader, Type objectType, State existingValue, bool hasExistingValue, JsonSerializer serializer) =>
                (bool)reader.Value ? State.On : State.Off;
            public override void WriteJson(JsonWriter writer, State value, JsonSerializer serializer) =>
                writer.WriteValue(value == State.On);
        }

        private class Report
        {
            [JsonProperty("power")]
            public double Power { get; }

            [JsonProperty("relay")]
            [JsonConverter(typeof(StateConverter))]
            public State State { get; }

            [JsonProperty("temperature")]
            public double Temperature { get; }

            [JsonConstructor]
            public Report(double power, State state, double temperature)
            {
                Power = power;
                State = state;
                Temperature = temperature;
            }
        }

        public MyStromSwitch()
        {
            AddAction("TurnOn", parameters => ChangeState(parameters[ParametersAddressKey], State.On));
            AddAction("TurnOff", parameters => ChangeState(parameters[ParametersAddressKey], State.Off));

            AddSensor("Power", parameters => GetReport(parameters[ParametersAddressKey]).Power.ToString("N2"));
            AddSensor("State", parameters => GetReport(parameters[ParametersAddressKey]).State.ToString());
            AddSensor("Temperature", parameters => GetReport(parameters[ParametersAddressKey]).Temperature.ToString("N2"));
        }

        private static IRestClient GetRestClient(string address) =>
            new RestClient(new UriBuilder(RestApiScheme, address).Uri);

        private static void ChangeState(string address, State state)
        {
            var client = GetRestClient(address);
            var request = new RestRequest("relay").AddParameter("state", (int)state);
            client.Get(request);
        }

        private static Report GetReport(string address)
        {
            var client = GetRestClient(address);
            var response = client.Get(new RestRequest("report"));
            return JsonConvert.DeserializeObject<Report>(response.Content);
        }
    }
}
