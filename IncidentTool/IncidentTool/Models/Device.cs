using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Models
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CurrentDeviceTypeId { get; set; }
    }
}
