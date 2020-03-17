using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Interfaces.Repositories
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri);
        Task PostAsync<T>(string uri, T data);
    }
}
