using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RLserver.Models.DTOs
{
    public class MatchDTO
    {
        public string Name { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}