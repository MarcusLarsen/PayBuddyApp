using PayBuddyApp.Interfaces;

namespace PayBuddyApp
{
    public partial class App : Application
    {
        public App(AppShell shell)
        {
            InitializeComponent();
            MainPage = shell;
        }
    }
}