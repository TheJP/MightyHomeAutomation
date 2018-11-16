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

        [HttpPost("{id}/actions/{action}/execute")]
        public ActionResult ExecuteAction(string id, string action, [FromBody] string input = "")
        {
            ActionResult CheckActionExists(DeviceType device)
            {
                if (!device.ContainsAction(action))
                {
                    return BadRequest("");
                }
                return null;
            }
            var result = CheckDeviceExists(id) ?? CheckActionExists(GetDeviceType(id));
            if (result == null)
            {
                GetDeviceType(id).ExecuteAction(action, input);
                result = Ok();
            }
            return result;
        }
    }
}