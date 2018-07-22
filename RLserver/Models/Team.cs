using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLserver.Models
{
    public class Team
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        public string Logo { get; set; }
        
        public List<ApplicationUser> Members { get; set; }
        
        public List<Tournament> Tournaments { get; set; }
        
        public List<Match> Matches { get; set; }

        public Team()
        {
            Members = new List<ApplicationUser>();
            Tournaments = new List<Tournament>();
            Matches = new List<Match>();
        }

        public Team(string name)
        {
            Name = name;
            Members = new List<ApplicationUser>();
            Tournaments = new List<Tournament>();
            Matches = new List<Match>();
        }
    }
}