using IncidentTool.Constants;
using IncidentTool.Interfaces.Repositories;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Services.Data
{
    public class DeviceDataService : IDeviceDataService
    {
        private readonly IGenericRepository _repository; // Data-bewerkingen wroden naar repository gestuurd

        public DeviceDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.DeviceByIdEndpoint(id)
            };

            var device = await _repository.GetAsync<Device>(builder.ToString());

            return device;
        }
    }
}
