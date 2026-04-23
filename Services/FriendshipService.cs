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

        public async Task<bool> DeleteFriendAsync(int friendshipId)
        {
            return await _apiService.DeleteAsync($"api/friendship/{friendshipId}", true);
        }

        public async Task<bool> AddFriendAsync(FriendForSaveDto dto)
        {
            var result = await _apiService.PostAsync<FriendForSaveDto, MessageResponseDto>("api/friendship", dto, true);
            return result != null;
        }
    }
}