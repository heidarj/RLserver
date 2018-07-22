using System.ComponentModel;

namespace RLserver.Models.DTOs
{
    public class TeamDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        [DefaultValue(new int[]{})]
        public string[] MemberIds { get; set; }
        [DefaultValue(new int[]{})]
        public int[] TournamentIds { get; set; }
        [DefaultValue(new int[]{})]
        public int[] MatchIds { get; set; }
        
        public string Logo { get; set; }  

        public string Description { get; set; }

        public string Slogan { get; set; }
    }
}