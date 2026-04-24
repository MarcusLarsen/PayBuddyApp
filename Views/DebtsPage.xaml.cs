using PayBuddyApp.ViewModels;

namespace PayBuddyApp.Views;

public partial class DebtsPage : ContentPage
{
    private readonly DebtsViewModel _viewModel;

    public DebtsPage(DebtsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDebtsAsync();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}