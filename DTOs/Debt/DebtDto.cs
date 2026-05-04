public class DebtDto
{
    public int DebtId { get; set; }

    public string? CreditorName { get; set; }
    public string? DebtorName { get; set; }

    public decimal Amount { get; set; }
    public string? Description { get; set; }

    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public bool CurrentUserIsCreditor { get; set; }
    public string DisplayText { get; set; } = string.Empty;

    public bool IsPaid => Status == "Paid";
    public bool IsAccepted => Status == "Accepted";
    public bool IsPending => Status == "Pending";
    public bool CanMarkAsPaid => CurrentUserIsCreditor && !IsPaid;

    public string StatusText =>
        Status == "Paid" ? "Betalt" :
        Status == "Accepted" ? "Aktiv" :
        Status == "Pending" ? "Afventer" : "Afvist";
}