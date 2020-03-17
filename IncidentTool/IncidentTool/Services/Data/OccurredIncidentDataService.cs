using IncidentTool.Constants;
using IncidentTool.Interfaces.Repositories;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Services.Data
{
    public class OccurredIncidentDataService : IOccurredIncidentDataService
    {
        private readonly IGenericRepository _repository; // Data-bewerkingen wroden naar repository gestuurd

        public OccurredIncidentDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<OccurredIncident>> GetAllOccurredIncidentsByUserIdAsync(int id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.OccurredIncidentsByUserIdEndpoint(id)
            };

            var occurredIncidens = await _repository.GetAsync<List<OccurredIncident>>(builder.ToString());

            return occurredIncidens;
        }

        public async Task CreateOccurredIncidentAsync(string description, int userId, int deviceId)
        {
            OccurredIncident occurredIncident = new OccurredIncident
            {
                OccurredIncidentId = await GenerateOccurredIncidentId(),
                IncidentDescription = description,
                CurrentUserId = userId,
                Solved = false,
                DeviceId = deviceId
            };

            await AddOccurredIncidentToDatabase(occurredIncident);
        }

        private async Task<int> GenerateOccurredIncidentId()
        {
            int count = (await GetAllOccurredIncidents()).ToList().Count;

            return count + 1;
        }

        private async Task<IList<OccurredIncident>> GetAllOccurredIncidents()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.OccurredIncidentsEndpoint
            };

            var occurredincidents = await _repository.GetAsync<List<OccurredIncident>>(builder.ToString());

            return occurredincidents;
        }

        private async Task AddOccurredIncidentToDatabase(OccurredIncident occurredIncident)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddOccurredIncidentEndpoint
            };

            await _repository.PostAsync<OccurredIncident>(builder.ToString(), occurredIncident);
        }
    }
}
