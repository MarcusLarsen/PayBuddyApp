using PayBuddyApp.Interfaces;
using PayBuddyApp.Views;

namespace PayBuddyApp.Controls;

public partial class BottomNavBar : ContentView
{
    public BottomNavBar()
    {
        InitializeComponent();

        Loaded += async (_, _) => await LoadNotificationCountAsync();
    }

    private async Task LoadNotificationCountAsync()
    {
        try
        {
            var services = Application.Current?.Handler?.MauiContext?.Services;

            if (services == null)
                return;

            var friendshipService = services.GetService<IFriendshipService>();
            var debtService = services.GetService<IDebtService>();

            if (friendshipService == null || debtService == null)
                return;

            var friendRequests = await friendshipService.GetFriendRequestsAsync();
            var debtRequests = await debtService.GetDebtRequestsAsync();

            var count = friendRequests.Count + debtRequests.Count;

            NotificationBadge.IsVisible = count > 0;
            NotificationBadgeText.Text = count > 99 ? "99+" : count.ToString();
        }
        catch
        {
            NotificationBadge.IsVisible = false;
        }
    }

    private async void OnHomeTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }

    private async void OnFriendsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FriendsPage));
    }

    private async void OnNotificationsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotificationsPage));
    }

    private async void OnFindTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FindFriendsPage));
    }

    private async void OnCreateDebtClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateDebtPage));
    }
}