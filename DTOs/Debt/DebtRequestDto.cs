using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.DTOs.Debt
{
    public class DebtRequestDto
    {
        public int DebtId { get; set; }

        public string? CreditorId { get; set; }
        public string? CreditorName { get; set; }

        public decimal Amount { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}