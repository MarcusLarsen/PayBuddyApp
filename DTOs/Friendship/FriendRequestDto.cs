using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.DTOs.Friendship
{
    public class FriendRequestDto
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
