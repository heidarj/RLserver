using System.Collections.Generic;

namespace RLserver.Models
{
    public class Match
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Team> Teams { get; set; }

        public Tournament Tournament { get; set; }

        public Match()
        {
            Teams = new List<Team>();
        }

        public Match(string name)
        {
            Name = name;
            Teams = new List<Team>();
        }
    }
}