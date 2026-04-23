using PayBuddyApp.Interfaces;
using PayBuddyApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PayBuddyApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
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

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            LoginCommand = new Command(async () => await LoginAsync());
            GoToRegisterCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(RegisterPage)));
        }

        private async Task LoginAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var success = await _authService.LoginAsync(new DTOs.Auth.LoginDto
            {
                Username = Username,
                Password = Password
            });

            IsBusy = false;

            if (success)
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Login fejlede", "OK");
            }             
        }
    }
}