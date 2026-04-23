using PayBuddyApp.Interfaces;
using PayBuddyApp.Views;

namespace PayBuddyApp
{
    public partial class AppShell : Shell
    {
        private readonly IAuthService _authService;
        private bool _hasCheckedLogin;

        public AppShell(IAuthService authService)
        {
            InitializeComponent();

            _authService = authService;

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_hasCheckedLogin)
                return;

            _hasCheckedLogin = true;

            var isLoggedIn = await _authService.HasValidTokenAsync();

            if (isLoggedIn)
            {
                await GoToAsync(nameof(HomePage));
            }
        }
    }
}