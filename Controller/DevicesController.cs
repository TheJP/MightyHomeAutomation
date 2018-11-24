using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger Logger { get; }

        public DevicesController(Configuration configuration, DeviceTypeManager deviceTypeManager, ILoggerFactory loggerFactory)
        {
            Devices = configuration.Devices.ToDictionary(device => device.Id);
            DeviceTypeManager = deviceTypeManager;
            Logger = loggerFactory.CreateLogger<DevicesController>();
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
        public ActionResult<string> ExecuteAction(string id, string actionName)
        {
            Logger.LogInformation("Execute action {Device}.{Action} by {IP}", id, actionName, HttpContext.Connection.RemoteIpAddress.ToString());
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
                GetDeviceType(id).ExecuteAction(actionName, Devices[id].Parameters);
                result = Ok("Success");
            }
            return result;
        }

        [HttpPost("{id}/sensors/{sensor}/read")]
        public ActionResult<string> ReadSensor(string id, string sensor)
        {
            Logger.LogInformation("Read sensor {Device}.{Sensor} by {IP}", id, sensor, HttpContext.Connection.RemoteIpAddress.ToString());
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
                Ok(GetDeviceType(id).ReadSensor(sensor, Devices[id].Parameters));
        }

        [HttpPost("readsensors")]
        public ActionResult<IEnumerable<string>> ReadSensors([FromBody] IList<string> queries)
        {
            Logger.LogInformation("Read sensors by {IP}", HttpContext.Connection.RemoteIpAddress.ToString());

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
            return Ok(tuples.Select(t => GetDeviceType(t.deviceId).ReadSensor(t.sensor, Devices[t.deviceId].Parameters)));
        }
    }
}
