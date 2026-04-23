using PayBuddyApp.Interfaces;

namespace PayBuddyApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            var authService = Application.Current?.Handler?.MauiContext?.Services.GetService<IAuthService>();

            if (authService != null)
            {
                await authService.LogoutAsync();
            }

            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}