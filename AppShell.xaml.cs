using PayBuddyApp.Interfaces;
using PayBuddyApp.Views;

namespace PayBuddyApp
{
    public partial class AppShell : Shell
    {
        public AppShell(IAuthService authService)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(FriendsPage), typeof(FriendsPage));
            Routing.RegisterRoute(nameof(FindFriendsPage), typeof(FindFriendsPage));
            Routing.RegisterRoute(nameof(CreateDebtPage), typeof(CreateDebtPage));
            Routing.RegisterRoute(nameof(DebtsPage), typeof(DebtsPage));
        }
    }
}