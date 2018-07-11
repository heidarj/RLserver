using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RLserver.Models
{
    public class Match
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<Tournament> Tournaments { get; set; }

        public Match()
        {
            Teams = new List<Team>();
            Tournaments = new List<Tournament>();
        }

        public Match(string name)
        {
            Name = name;
            Teams = new List<Team>();
            Tournaments = new List<Tournament>();
        }
    }
}