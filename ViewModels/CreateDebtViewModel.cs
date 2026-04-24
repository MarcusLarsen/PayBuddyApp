using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class CreateDebtViewModel : BaseViewModel
    {
        private readonly IDebtService _debtService;
        private readonly IFriendshipService _friendshipService;

        public ObservableCollection<FriendDto> Friends { get; } = new();

        private FriendDto? _selectedFriend;
        public FriendDto? SelectedFriend
        {
            get => _selectedFriend;
            set => SetProperty(ref _selectedFriend, value);
        }

        private string _amountText = string.Empty;
        public string AmountText
        {
            get => _amountText;
            set => SetProperty(ref _amountText, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public ICommand CreateDebtCommand { get; }

        public CreateDebtViewModel(IDebtService debtService, IFriendshipService friendshipService)
        {
            _debtService = debtService;
            _friendshipService = friendshipService;

            CreateDebtCommand = new Command(async () => await CreateDebtAsync());
        }

        public async Task LoadFriendsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Friends.Clear();

                var friends = await _friendshipService.GetFriendsAsync();

                foreach (var friend in friends)
                {
                    Friends.Add(friend);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CreateDebtAsync()
        {
            if (IsBusy)
                return;

            if (SelectedFriend == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Vælg en ven.", "OK");
                return;
            }

            if (!decimal.TryParse(AmountText, out var amount) || amount <= 0)
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Indtast et gyldigt beløb.", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var dto = new DebtForSaveDto
                {
                    DebtorId = SelectedFriend.FriendId,
                    Amount = amount,
                    Description = Description
                };

                var success = await _debtService.CreateDebtAsync(dto);

                if (success)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Succes", "Gæld oprettet.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Fejl", "Kunne ikke oprette gæld.", "OK");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}