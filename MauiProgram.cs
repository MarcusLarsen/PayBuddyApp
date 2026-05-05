using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using PayBuddyApp.Interfaces;
using PayBuddyApp.Services;
using PayBuddyApp.ViewModels;
using PayBuddyApp.Views;

namespace PayBuddyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<IApiService, ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://paybuddyapi.onrender.com/swagger/index.html");
            });

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IDebtService, DebtService>();
            builder.Services.AddScoped<IFriendshipService, FriendshipService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<FriendsViewModel>();
            builder.Services.AddTransient<FindFriendsViewModel>();
            builder.Services.AddTransient<DebtsViewModel>();
            builder.Services.AddTransient<CreateDebtViewModel>();
            builder.Services.AddTransient<NotificationsViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<FriendsPage>();
            builder.Services.AddTransient<FindFriendsPage>();
            builder.Services.AddTransient<CreateDebtPage>();
            builder.Services.AddTransient<DebtsPage>();
            builder.Services.AddTransient<CreateDebtPage>();
            builder.Services.AddTransient<NotificationsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}