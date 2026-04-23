using PayBuddyApp.DTOs.Auth;
using PayBuddyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _apiService;

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var result = await _apiService.PostAsync<LoginDto, AuthResponseDto>("api/auth/login", dto);

            if (result is null || string.IsNullOrWhiteSpace(result.Token))
                return false;

            await SecureStorage.Default.SetAsync("auth_token", result.Token);
            await SecureStorage.Default.SetAsync("username", result.Username);

            return true;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var result = await _apiService.PostAsync<RegisterDto, AuthResponseDto>("api/auth/register", dto);

            if (result == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Debug", "result er null", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(result.Token))
            {
                await Application.Current!.MainPage!.DisplayAlert("Debug", "Token er null eller tom", "OK");
                return false;
            }

            await SecureStorage.Default.SetAsync("auth_token", result.Token);

            if (!string.IsNullOrWhiteSpace(result.Username))
            {
                await SecureStorage.Default.SetAsync("username", result.Username);
            }

            return true;
        }

        public async Task<bool> HasValidTokenAsync()
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");
            return !string.IsNullOrWhiteSpace(token);
        }

        public Task LogoutAsync()
        {
            SecureStorage.Default.Remove("auth_token");
            SecureStorage.Default.Remove("username");
            return Task.CompletedTask;
        }
    }
}
