using System.ComponentModel;

namespace RLserver.Models.DTOs
{
    public class UserDetailsDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfilePicBase64Enc { get; set; }
        [DefaultValue(new int[]{})]
        public int[] TeamIds { get; set; }
        public string Description { get; set; }
        public string Slogan { get; set; }
        public string FacebookProfileUri { get; set; }       
    }
}