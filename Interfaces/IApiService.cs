using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Interfaces
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint, bool authorized = false);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, bool authorized = false);
    }
}
