using PayBuddyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task AddAuthHeaderAsync(bool authorized)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!authorized)
                return;

            var token = await SecureStorage.Default.GetAsync("auth_token");

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T?> GetAsync<T>(string endpoint, bool authorized = false)
        {
            await AddAuthHeaderAsync(authorized);
            return await _httpClient.GetFromJsonAsync<T>(endpoint);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, bool authorized = false)
        {
            try
            {
                await AddAuthHeaderAsync(authorized);

                var response = await _httpClient.PostAsJsonAsync(endpoint, data);

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"DEBUG URL: {_httpClient.BaseAddress}{endpoint}");
                Console.WriteLine($"DEBUG STATUS: {response.StatusCode}");
                Console.WriteLine($"DEBUG RESPONSE: {content}");

                return await response.Content.ReadFromJsonAsync<TResponse>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG ERROR: {ex.Message}");
                throw;
            }

        }

        public async Task<bool> PostAsync<TRequest>(string endpoint, TRequest data, bool authorized = false)
        {
            try
            {
                await AddAuthHeaderAsync(authorized);

                var response = await _httpClient.PostAsJsonAsync(endpoint, data);

                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"DEBUG URL: {_httpClient.BaseAddress}{endpoint}");
                Console.WriteLine($"DEBUG STATUS: {response.StatusCode}");
                Console.WriteLine($"DEBUG RESPONSE: {content}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG REGISTER ERROR: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> PutAsync<TRequest>(string endpoint, TRequest data, bool authorized = false)
        {
            await AddAuthHeaderAsync(authorized);

            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint, bool authorized = false)
        {
            await AddAuthHeaderAsync(authorized);

            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}