using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.Services
{
    public class DebtService : IDebtService
    {
        private readonly IApiService _apiService;

        public DebtService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<DebtDto>> GetUserDebtsAsync()
        {
            var result = await _apiService.GetAsync<List<DebtDto>>("api/debt/GetUserDebts", true);
            return result ?? new List<DebtDto>();
        }
    }
}