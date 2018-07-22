using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Configuration;
using Newtonsoft.Json;


namespace RLserver.Models.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [DefaultValue(new int[]{})]
        public int[] TeamIds { get; set; }

        public int TournamentId { get; set; }
    }
}