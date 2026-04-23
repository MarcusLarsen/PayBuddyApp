using PayBuddyApp.ViewModels;

namespace PayBuddyApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }

    private async void OnCreateDebtClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateDebtPage));
    }

    private async void OnOpenFriendsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FriendsPage));
    }

    private async void OnOpenDebtsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DebtsPage));
    }

    private async void OnOpenFriendsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FriendsPage));
    }

    private async void OnOpenFindFriendsTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FindFriendsPage));
    }

    private async void OnProfileTapped(object sender, TappedEventArgs e)
    {
        await ShowProfileMenu();
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await ShowProfileMenu();
    }

    private async Task ShowProfileMenu()
    {
        var action = await DisplayActionSheet("Profil", "Annuller", null, "Log ud", "Slet profil");

        if (action == "Log ud")
        {
            await _viewModel.LogoutAsync();
        }
        else if (action == "Slet profil")
        {
            await DisplayAlert("Info", "Den kobler vi på UserController bagefter.", "OK");
        }
    }
}