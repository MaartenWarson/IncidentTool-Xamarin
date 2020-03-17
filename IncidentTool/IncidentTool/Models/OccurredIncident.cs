using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTool.Models
{
    public class OccurredIncident
    {
        public int OccurredIncidentId { get; set; }
        public string IncidentDescription { get; set; }
        public int CurrentUserId { get; set; }
        public bool Solved { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; } // Extra
        public string DeviceLocation { get; set; } // Extra
        public string SolvedString { get; set; } // Extra
    }
}
