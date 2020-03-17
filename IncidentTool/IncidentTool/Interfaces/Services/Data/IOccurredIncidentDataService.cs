using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.Data
{
    public interface IOccurredIncidentDataService
    {
        Task<IList<OccurredIncident>> GetAllOccurredIncidentsByUserIdAsync(int id);
        Task CreateOccurredIncidentAsync(string description, int userId, int deviceId);
    }
}
