using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IDebtService _debtService;
        private readonly IFriendshipService _friendshipService;
        private readonly IAuthService _authService;

        public ObservableCollection<DebtDto> Debts { get; } = new();
        public ObservableCollection<FriendDto> Friends { get; } = new();

        private decimal _moneyOwedToYou;
        public decimal MoneyOwedToYou
        {
            get => _moneyOwedToYou;
            set => SetProperty(ref _moneyOwedToYou, value);
        }

        private decimal _moneyYouOwe;
        public decimal MoneyYouOwe
        {
            get => _moneyYouOwe;
            set => SetProperty(ref _moneyYouOwe, value);
        }

        public HomeViewModel(
            IDebtService debtService,
            IFriendshipService friendshipService,
            IAuthService authService)
        {
            _debtService = debtService;
            _friendshipService = friendshipService;
            _authService = authService;
        }

        public async Task LoadDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Debts.Clear();
                Friends.Clear();

                var debts = await _debtService.GetUserDebtsAsync();
                var friends = await _friendshipService.GetFriendsAsync();

                foreach (var debt in debts)
                    Debts.Add(debt);

                foreach (var friend in friends)
                    Friends.Add(friend);

                CalculateTotals();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void CalculateTotals()
        {
            MoneyOwedToYou = Debts
                .Where(d => !d.IsPaid)
                .Where(d => d.CreditorName != null && d.DebtorName != null)
                .Sum(d => d.Description != null && d.CreditorName != d.DebtorName ? d.Amount : d.Amount);

            MoneyYouOwe = 0;
        }

        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}