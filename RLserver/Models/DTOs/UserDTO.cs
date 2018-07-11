using System.Collections.Generic;

namespace RLserver.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfilePicBase64Enc { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}