using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MightyHomeAutomation.Logic.Devices;
using MightyHomeAutomation.Persistence;

namespace MightyHomeAutomation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private IDictionary<string, Device> Devices { get; }
        private DeviceTypeManager DeviceTypeManager { get; }

        public DevicesController(Configuration configuration, DeviceTypeManager deviceTypeManager)
        {
            Devices = configuration.Devices.ToDictionary(device => device.Id);
            DeviceTypeManager = deviceTypeManager;
        }

        private DeviceType GetDeviceType(string id) => DeviceTypeManager.Devices[Devices[id].Type];

        public IEnumerable<string> GetAll() => Devices.Keys;

        private ActionResult CheckDeviceExists(string id)
        {
            if (!Devices.ContainsKey(id))
            {
                return BadRequest("Device with given id does not exist");
            }
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<Device> Get(string id) =>
            CheckDeviceExists(id) ?? Ok(new { Id = id, Type = Devices[id].Type });

        [HttpGet("{id}/actions")]
        public ActionResult<IEnumerable<string>> GetActions(string id) =>
            CheckDeviceExists(id) ?? Ok(GetDeviceType(id).Actions);

        [HttpGet("{id}/sensors")]
        public ActionResult<IEnumerable<string>> GetSensors(string id) =>
            CheckDeviceExists(id) ?? Ok(GetDeviceType(id).Sensors);

        [HttpPost("{id}/actions/{actionName}/execute")]
        public ActionResult ExecuteAction(string id, string actionName, [FromBody] string input = "")
        {
            ActionResult CheckActionExists(DeviceType device)
            {
                if (!device.ContainsAction(actionName))
                {
                    return BadRequest("Action with given name does not exist for device with given id");
                }
                return null;
            }

            var result = CheckDeviceExists(id) ?? CheckActionExists(GetDeviceType(id));
            if (result == null)
            {
                GetDeviceType(id).ExecuteAction(actionName, input);
                result = Ok();
            }
            return result;
        }

        [HttpPost("{id}/sensors/{sensor}/read")]
        public ActionResult<string> ReadSensor(string id, string sensor, [FromBody] string input = "")
        {
            ActionResult CheckSensorExists(DeviceType device)
            {
                if (!device.ContainsSensor(sensor))
                {
                    return BadRequest("Sensor with given name does not exist for device with given id");
                }
                return null;
            }

            return CheckDeviceExists(id) ??
                CheckSensorExists(GetDeviceType(id)) ??
                Ok(GetDeviceType(id).ReadSensor(sensor, input));
        }

        [HttpPost("readsensors")]
        public ActionResult<IEnumerable<string>> ReadSensors([FromBody] IList<string> queries)
        {
            // Check if queries have correct format
            var split = queries.Select(query => query.Split('.'));
            if (split.Any(s => s.Length != 2))
            {
                return BadRequest("Invalid query string");
            }
            var tuples = split.Select(s => (deviceId: s[0], sensor: s[1]));

            // Check if device and sensor exist for each query
            if (tuples.Any(t => !Devices.ContainsKey(t.deviceId) ||
                !GetDeviceType(t.deviceId).ContainsSensor(t.sensor)))
            {
                return BadRequest("Device with given id does not exist or does not have sensor with given name");
            }

            // Read sensors for each query
            return Ok(tuples.Select(t => GetDeviceType(t.deviceId).ReadSensor(t.sensor)));
        }
    }
}
