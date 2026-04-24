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

        public async Task<bool> CreateDebtAsync(DebtForSaveDto dto)
        {
            return await _apiService.PostAsync("api/debt/CreateDebt", dto, true);
        }

        public async Task<bool> MarkAsPaidAsync(int debtId)
        {
            return await _apiService.PutAsync($"api/debt/MarkAsPaid/{debtId}", new { }, true);
        }
    }
}