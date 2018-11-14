using System;

namespace MightyHomeAutomation.Logic.Devices
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class RegisterDeviceTypeAttribute : Attribute
    {
        public string DeviceTypeId { get; }

        public RegisterDeviceTypeAttribute(string deviceTypeId) => DeviceTypeId = deviceTypeId;
    }
}