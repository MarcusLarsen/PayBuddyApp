using PayBuddyApp.DTOs.Debt;

namespace PayBuddyApp.Interfaces
{
    public interface IDebtService
    {
        Task<List<DebtDto>> GetUserDebtsAsync();
        Task<List<DebtRequestDto>> GetDebtRequestsAsync();

        Task<bool> CreateDebtAsync(DebtForSaveDto dto);
        Task<bool> AcceptDebtAsync(int debtId);
        Task<bool> DeclineDebtAsync(int debtId);
        Task<bool> MarkAsPaidAsync(int debtId);
    }
}