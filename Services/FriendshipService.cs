using PayBuddyApp.DTOs.Friendship;
using PayBuddyApp.DTOs.Responses;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IApiService _apiService;

        public FriendshipService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<FriendDto>> GetFriendsAsync()
        {
            var result = await _apiService.GetAsync<List<FriendDto>>("api/friendship", true);
            return result ?? new List<FriendDto>();
        }

        public async Task<List<FriendRequestDto>> GetFriendRequestsAsync()
        {
            var result = await _apiService.GetAsync<List<FriendRequestDto>>("api/friendship/requests", true);
            return result ?? new List<FriendRequestDto>();
        }

        public async Task<bool> SendFriendRequestAsync(FriendForSaveDto dto)
        {
            var result = await _apiService.PostAsync<FriendForSaveDto, MessageResponseDto>(
                "api/friendship/request",
                dto,
                true);

            return result != null;
        }

        public async Task<bool> AcceptFriendRequestAsync(int friendshipId)
        {
            return await _apiService.PutAsync($"api/friendship/accept/{friendshipId}", new { }, true);
        }

        public async Task<bool> DeclineFriendRequestAsync(int friendshipId)
        {
            return await _apiService.PutAsync($"api/friendship/decline/{friendshipId}", new { }, true);
        }

        public async Task<bool> DeleteFriendAsync(int friendshipId)
        {
            return await _apiService.DeleteAsync($"api/friendship/{friendshipId}", true);
        }
    }
}