using PayBuddyApp.ViewModels;

namespace PayBuddyApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}