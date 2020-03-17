using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Services.Data
{
    public interface IDeviceDataService
    {
        Task<Device> GetDeviceByIdAsync(int id);
    }
}
