using PayBuddyApp.DTOs.User;
using PayBuddyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Services
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<UserDto>> SearchUsersAsync(string searchTerm)
        {
            string endpoint;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                endpoint = "api/user/SearchUsers?searchTerm=";
            }
            else
            {
                endpoint = $"api/user/SearchUsers?searchTerm={Uri.EscapeDataString(searchTerm)}";
            }

            var result = await _apiService.GetAsync<List<UserDto>>(endpoint, true);

            return result ?? new List<UserDto>();
        }
    }
}