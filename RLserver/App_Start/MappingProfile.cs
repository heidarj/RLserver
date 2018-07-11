using AutoMapper;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();

            CreateMap<Match, MatchDTO>();
            CreateMap<MatchDTO, Match>();

            CreateMap<Tournament, TournamentDTO>();
            CreateMap<TournamentDTO, Tournament>();

            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}