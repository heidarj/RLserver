using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RLserver.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Team> Teams { get; set; }
        
        [Required]
        public TournamentType TournamentType { get; set; }
        
        // ReSharper disable once InconsistentNaming
        private List<Round> _matches { get; set; }

        public List<Round> Matches
        {
            get
            {
                if (_matches != null)
                {
                    return _matches;
                }
                else
                {
                    _matches = GenerateMatchList();
                    return _matches;
                }
            }
        }
        
        public int CurrentRound { get; set; }
        public int TotalRounds { get; set; }



        public Tournament()
        {
            Teams = new List<Team>();
            CurrentRound = 0;
            TotalRounds = 0;
        }


        public List<Round> GenerateMatchList()
        {
            switch (TournamentType)
            {
                    case TournamentType.AllVersusAll:
                        return GenerateAllVersusAll();

                    case TournamentType.Brackets:
                        return GenerateBrackets();
                    
                    case TournamentType.Groups:
                        return GenerateGroups();

                    default:
                        return new List<Round>();
            }
        }

        private List<Round> GenerateGroups()
        {
            return new List<Round>();
        }

        private List<Round> GenerateBrackets()
        {
            return new List<Round>();
        }

        private List<Round> GenerateAllVersusAll()
        {
            return new List<Round>();
        }
 
    }

    public enum TournamentType
    {   // 0
        AllVersusAll,
        // 1
        Brackets,
        // 2
        Groups
    }

    public class Round
    {
        public int Id { get; set; }
        public List<Match> Matches { get; set; }
    }
}