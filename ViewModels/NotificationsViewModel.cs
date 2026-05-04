using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IDebtService _debtService;

        public ObservableCollection<FriendRequestDto> FriendRequests { get; } = new();
        public ObservableCollection<DebtRequestDto> DebtRequests { get; } = new();

        public ICommand AcceptFriendCommand { get; }
        public ICommand DeclineFriendCommand { get; }

        public ICommand AcceptDebtCommand { get; }
        public ICommand DeclineDebtCommand { get; }

        public NotificationsViewModel(
            IFriendshipService friendshipService,
            IDebtService debtService)
        {
            _friendshipService = friendshipService;
            _debtService = debtService;

            AcceptFriendCommand = new Command<FriendRequestDto>(async r => await AcceptFriend(r));
            DeclineFriendCommand = new Command<FriendRequestDto>(async r => await DeclineFriend(r));

            AcceptDebtCommand = new Command<DebtRequestDto>(async r => await AcceptDebt(r));
            DeclineDebtCommand = new Command<DebtRequestDto>(async r => await DeclineDebt(r));
        }

        public async Task LoadAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                FriendRequests.Clear();
                DebtRequests.Clear();

                var friends = await _friendshipService.GetFriendRequestsAsync();
                var debts = await _debtService.GetDebtRequestsAsync();

                foreach (var f in friends)
                    FriendRequests.Add(f);

                foreach (var d in debts)
                    DebtRequests.Add(d);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AcceptFriend(FriendRequestDto? req)
        {
            if (req == null) return;

            var success = await _friendshipService.AcceptFriendRequestAsync(req.Id);

            if (success)
                FriendRequests.Remove(req);
        }

        private async Task DeclineFriend(FriendRequestDto? req)
        {
            if (req == null) return;

            var success = await _friendshipService.DeclineFriendRequestAsync(req.Id);

            if (success)
                FriendRequests.Remove(req);
        }

        private async Task AcceptDebt(DebtRequestDto? req)
        {
            if (req == null) return;

            var success = await _debtService.AcceptDebtAsync(req.DebtId);

            if (success)
                DebtRequests.Remove(req);
        }

        private async Task DeclineDebt(DebtRequestDto? req)
        {
            if (req == null) return;

            var success = await _debtService.DeclineDebtAsync(req.DebtId);

            if (success)
                DebtRequests.Remove(req);
        }
    }
}