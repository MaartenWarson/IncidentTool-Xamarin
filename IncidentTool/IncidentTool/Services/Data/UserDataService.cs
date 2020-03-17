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
    public class UserDataService : IUserDataService
    {
        private readonly IGenericRepository _repository; // Data-bewerkingen wroden naar repository gestuurd

        public UserDataService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.UserByNameEndpoint(name)
            };

            User user = await _repository.GetAsync<User>(builder.ToString());

            return user;
        }
    }
}
