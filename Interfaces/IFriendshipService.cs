using PayBuddyApp.DTOs.Friendship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.Interfaces
{
    public interface IFriendshipService
    {
        Task<List<FriendDto>> GetFriendsAsync();
        Task<bool> DeleteFriendAsync(int friendshipId);
        Task<bool> AddFriendAsync(FriendForSaveDto dto);
    }
}
