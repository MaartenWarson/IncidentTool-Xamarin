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
    public class IncidentDataService : IIncidentDataService
    {
        private readonly IGenericRepository _repository; // Data-bewerkingen wroden naar repository gestuurd

        public IncidentDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Incident>> GetAllIncidentsByDeviceTypeId(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.IncidentsByDeviceTypeId(id)
            };

            var incidents = await _repository.GetAsync<List<Incident>>(builder.ToString());

            return incidents;
        }
    }
}
