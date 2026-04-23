using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBuddyApp.DTOs.Friendship
{
    public class FriendDto
    {
        public int Id { get; set; }

        public string? FriendId { get; set; }

        public string? FriendUserName { get; set; }
    }
}