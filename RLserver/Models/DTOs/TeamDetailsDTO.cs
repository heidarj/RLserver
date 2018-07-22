using System.Collections.Generic;
using System.ComponentModel;

namespace RLserver.Models.DTOs
{
    public class TeamDetailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        public string Logo { get; set; }
        
        public List<UserDTO> Members { get; set; }
        
        public List<Tournament> Tournaments { get; set; }
        
        public List<Match> Matches { get; set; }
    }
}