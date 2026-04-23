using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private readonly IFriendshipService _friendshipService;

        public ObservableCollection<FriendDto> Friends { get; } = new();

        public ICommand RefreshCommand { get; }
        public ICommand RemoveFriendCommand { get; }

        public FriendsViewModel(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;

            RefreshCommand = new Command(async () => await LoadFriendsAsync());
            RemoveFriendCommand = new Command<FriendDto>(async (friend) => await RemoveFriendAsync(friend));
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
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", $"Kunne ikke hente venner: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RemoveFriendAsync(FriendDto? friend)
        {
            if (friend == null)
                return;

            var confirm = await Application.Current!.MainPage!.DisplayAlert(
                "Fjern ven",
                $"Vil du fjerne {friend.FriendUserName}?",
                "Ja",
                "Nej");

            if (!confirm)
                return;

            var success = await _friendshipService.DeleteFriendAsync(friend.Id);

            if (success)
            {
                Friends.Remove(friend);
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Kunne ikke fjerne ven.", "OK");
            }
        }
    }
}
