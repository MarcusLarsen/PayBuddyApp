using PayBuddyApp.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);
        Task<bool> HasValidTokenAsync();
        Task LogoutAsync();
    }
}
