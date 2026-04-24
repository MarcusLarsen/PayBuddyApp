using PayBuddyApp.DTOs.Debt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Interfaces
{
    public interface IDebtService
    {
        Task<List<DebtDto>> GetUserDebtsAsync();
        Task<bool> CreateDebtAsync(DebtForSaveDto dto);
        Task<bool> MarkAsPaidAsync(int debtId);
    }
}
