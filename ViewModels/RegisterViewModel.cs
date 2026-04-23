using System.Windows.Input;
using PayBuddyApp.DTOs.Auth;
using PayBuddyApp.Interfaces;
using PayBuddyApp.Views;

namespace PayBuddyApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
            RegisterCommand = new Command(async () => await RegisterAsync());
            GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync("//LoginPage"));
        }

        private async Task RegisterAsync()
        {
            if (IsBusy)
                return;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Udfyld alle felter.", "OK");
                return;
            }

            IsBusy = true;

            var success = await _authService.RegisterAsync(new RegisterDto
            {
                Username = Username,
                Password = Password
            });

            IsBusy = false;

            if (success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Succes", "Bruger oprettet.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Registrering fejlede.", "OK");
            }
        }
    }
}
