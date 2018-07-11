using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLserver.Models.DTOs
{
    public class TournamentDTO
    {
        public ICollection<Team> Teams { get; set; }
        public TournamentType TournamentType { get; set; }
        // ReSharper disable once InconsistentNaming
        private List<Round> _matches { get; set; }

        public List<Round> Matches { get; set; }
    }
}