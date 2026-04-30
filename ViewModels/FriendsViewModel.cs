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
        public ObservableCollection<FriendRequestDto> FriendRequests { get; } = new();

        public ICommand RefreshCommand { get; }
        public ICommand RemoveFriendCommand { get; }
        public ICommand AcceptRequestCommand { get; }
        public ICommand DeclineRequestCommand { get; }

        public FriendsViewModel(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;

            RefreshCommand = new Command(async () => await LoadFriendsAsync());
            RemoveFriendCommand = new Command<FriendDto>(async (friend) => await RemoveFriendAsync(friend));
            AcceptRequestCommand = new Command<FriendRequestDto>(async (request) => await AcceptRequestAsync(request));
            DeclineRequestCommand = new Command<FriendRequestDto>(async (request) => await DeclineRequestAsync(request));
        }

        public async Task LoadFriendsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Friends.Clear();
                FriendRequests.Clear();

                var friends = await _friendshipService.GetFriendsAsync();
                var requests = await _friendshipService.GetFriendRequestsAsync();

                foreach (var friend in friends)
                    Friends.Add(friend);

                foreach (var request in requests)
                    FriendRequests.Add(request);
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Fejl",
                    $"Kunne ikke hente venner: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AcceptRequestAsync(FriendRequestDto? request)
        {
            if (request == null)
                return;

            var success = await _friendshipService.AcceptFriendRequestAsync(request.Id);

            if (success)
            {
                FriendRequests.Remove(request);
                await LoadFriendsAsync();

                await Application.Current!.MainPage!.DisplayAlert(
                    "Succes",
                    "Venneanmodning accepteret.",
                    "OK");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Fejl",
                    "Kunne ikke acceptere venneanmodning.",
                    "OK");
            }
        }

        private async Task DeclineRequestAsync(FriendRequestDto? request)
        {
            if (request == null)
                return;

            var success = await _friendshipService.DeclineFriendRequestAsync(request.Id);

            if (success)
            {
                FriendRequests.Remove(request);

                await Application.Current!.MainPage!.DisplayAlert(
                    "Afvist",
                    "Venneanmodning afvist.",
                    "OK");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Fejl",
                    "Kunne ikke afvise venneanmodning.",
                    "OK");
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
                await Application.Current!.MainPage!.DisplayAlert(
                    "Fejl",
                    "Kunne ikke fjerne ven.",
                    "OK");
            }
        }
    }
}