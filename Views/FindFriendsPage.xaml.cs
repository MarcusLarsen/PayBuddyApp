using PayBuddyApp.ViewModels;

namespace PayBuddyApp.Views;

public partial class FindFriendsPage : ContentPage
{
    private readonly FindFriendsViewModel _viewModel;

    public FindFriendsPage(FindFriendsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUsersAsync();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}