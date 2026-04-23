using PayBuddyApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> SearchUsersAsync(string searchTerm);
    }
}
