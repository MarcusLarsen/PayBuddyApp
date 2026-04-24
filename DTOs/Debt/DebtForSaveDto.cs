using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.DTOs.Debt
{
    public class DebtForSaveDto
    {
        public string? DebtorId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
