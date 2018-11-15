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

        public IEnumerable<Device> GetAll() => Devices.Values;

        [HttpGet("{id}/actions")]
        public ActionResult<IEnumerable<string>> GetActions(string id)
        {
            if (!Devices.ContainsKey(id))
            {
                return BadRequest("Device with given id does not exist");
            }
            // If the following access failes, the server has an error in the configuration.
            // So it is correct to throw an exception.
            return Ok(DeviceTypeManager.Devices[Devices[id].Type].Actions.Keys);
        }

        [HttpGet("{id}/sensors")]
        public ActionResult<IEnumerable<string>> GetSensors(string id)
        {
            if (!Devices.ContainsKey(id))
            {
                return BadRequest("Device with given id does not exist");
            }
            // If the following access failes, the server has an error in the configuration.
            // So it is correct to throw an exception.
            return Ok(DeviceTypeManager.Devices[Devices[id].Type].Sensors.Keys);
        }
    }
}