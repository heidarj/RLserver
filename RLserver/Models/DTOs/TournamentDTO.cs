using System.Collections.Generic;
using System.ComponentModel;

namespace RLserver.Models.DTOs
{
    public class TournamentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [DefaultValue(new int[]{})]
        public int[] TeamIds { get; set; }

        public TournamentType TournamentType { get; set; }

        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }
    }
}