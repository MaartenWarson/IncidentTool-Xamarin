using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Models
{
    public class Incident
    {
        public int IncidentId { get; set; }
        public string Description { get; set; }
        public int CurrentDeviceTypeId { get; set; }
    }
}
