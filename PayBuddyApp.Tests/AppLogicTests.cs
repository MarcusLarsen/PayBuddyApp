namespace PayBuddyApp.Tests;

public class AppLogicTests
{
    [Fact]
    public void StatusText_Should_Return_Betalt_When_Status_Is_Paid()
    {
        var debt = new TestDebtDto
        {
            Status = "Paid"
        };

        Assert.True(debt.IsPaid);
        Assert.Equal("Betalt", debt.StatusText);
    }

    [Fact]
    public void CanMarkAsPaid_Should_Be_True_When_User_Is_Creditor_And_Debt_Is_Not_Paid()
    {
        var debt = new TestDebtDto
        {
            Status = "Accepted",
            CurrentUserIsCreditor = true
        };

        Assert.True(debt.CanMarkAsPaid);
    }

    [Fact]
    public void CalculateTotals_Should_Calculate_MoneyOwedToYou()
    {
        var debts = new List<TestDebtDto>
        {
            new TestDebtDto
            {
                Amount = 100,
                Status = "Accepted",
                CurrentUserIsCreditor = true
            }
        };

        var moneyOwedToYou = debts
            .Where(d => !d.IsPaid)
            .Where(d => d.CurrentUserIsCreditor)
            .Sum(d => d.Amount);

        Assert.Equal(100, moneyOwedToYou);
    }

    [Fact]
    public void CalculateTotals_Should_Calculate_MoneyYouOwe()
    {
        var debts = new List<TestDebtDto>
        {
            new TestDebtDto
            {
                Amount = 250,
                Status = "Accepted",
                CurrentUserIsCreditor = false
            }
        };

        var moneyYouOwe = debts
            .Where(d => !d.IsPaid)
            .Where(d => !d.CurrentUserIsCreditor)
            .Sum(d => d.Amount);

        Assert.Equal(250, moneyYouOwe);
    }
}

public class TestDebtDto
{
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool CurrentUserIsCreditor { get; set; }

    public bool IsPaid => Status == "Paid";
    public bool CanMarkAsPaid => CurrentUserIsCreditor && !IsPaid;

    public string StatusText =>
        Status == "Paid" ? "Betalt" :
        Status == "Accepted" ? "Aktiv" :
        Status == "Pending" ? "Afventer" :
        "Afvist";
}