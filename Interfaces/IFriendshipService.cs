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
        Task<List<FriendRequestDto>> GetFriendRequestsAsync();

        Task<bool> SendFriendRequestAsync(FriendForSaveDto dto);
        Task<bool> AcceptFriendRequestAsync(int friendshipId);
        Task<bool> DeclineFriendRequestAsync(int friendshipId);

        Task<bool> DeleteFriendAsync(int friendshipId);
    }
}