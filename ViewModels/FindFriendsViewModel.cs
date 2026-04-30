using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.DTOs.User;
using PayBuddyApp.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PayBuddyApp.ViewModels
{
    public class FindFriendsViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IFriendshipService _friendshipService;

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value);
        }

        public ObservableCollection<UserDto> SearchResults { get; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddFriendCommand { get; }

        public FindFriendsViewModel(IUserService userService, IFriendshipService friendshipService)
        {
            _userService = userService;
            _friendshipService = friendshipService;

            SearchCommand = new Command(async () => await LoadUsersAsync());
            AddFriendCommand = new Command<UserDto>(async (user) => await AddFriendAsync(user));
        }

        public async Task LoadUsersAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SearchResults.Clear();

                var users = await _userService.SearchUsersAsync(SearchTerm);

                foreach (var user in users)
                {
                    SearchResults.Add(user);
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", $"Kunne ikke hente brugere: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task AddFriendAsync(UserDto? user)
        {
            if (user == null)
                return;

            var success = await _friendshipService.SendFriendRequestAsync(new FriendForSaveDto
            {
                FriendUserName = user.UserName
            });

            if (success)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Succes",
                    $"Venneanmodning sendt til {user.UserName}.",
                    "OK"
                );

                SearchResults.Remove(user);
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Fejl",
                    "Kunne ikke sende venneanmodning.",
                    "OK"
                );
            }
        }
    }
}