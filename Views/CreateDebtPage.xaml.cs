using PayBuddyApp.ViewModels;

namespace PayBuddyApp.Views;

public partial class CreateDebtPage : ContentPage
{
    private readonly CreateDebtViewModel _viewModel;

    public CreateDebtPage(CreateDebtViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadFriendsAsync();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}