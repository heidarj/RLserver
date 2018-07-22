using AutoMapper;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tournament, int>().ConvertUsing(dest => dest.Id);

            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();

            CreateMap<Team, TeamDetailsDTO>();
            CreateMap<TeamDetailsDTO, Team>();

            CreateMap<Match, MatchDTO>();
            CreateMap<MatchDTO, Match>();

            CreateMap<Tournament, TournamentDTO>();
            CreateMap<TournamentDTO, Tournament>();

            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
        }
    }
}