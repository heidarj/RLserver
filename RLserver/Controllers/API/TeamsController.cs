using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Mvc;
using AutoMapper;
using RLserver.Models;
using RLserver.Models.DTOs;
using IModelBinder = System.Web.Http.ModelBinding.IModelBinder;
using ModelBindingContext = System.Web.Http.ModelBinding.ModelBindingContext;

namespace RLserver.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/teams")]
    public class TeamsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Teams and api/Teams?id=19,20
        [System.Web.Http.Route("")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get([System.Web.Http.ModelBinding.ModelBinder(typeof(CommaDelimitedArrayModelBinder))]int[] id = null)
        {
            if (id == null)
            {
                var allTeams = _db.Teams
                    .Include("Members")
                    .Include("Tournaments")
                    .AsEnumerable()
                    .Select(Mapper.Map<TeamDTO>);
                
                return Ok(allTeams);
            }

            if (id.Length <= 0) return BadRequest();

            var teams = _db.Teams
                .Include("Members")
                .Include("Tournaments")
                .AsEnumerable()
                .Select(Mapper.Map<TeamDTO>).Where(x => id.Contains(x.Id))
                .ToList();
            if (teams.Any())
            {
                return Ok(teams);
            }

            return NotFound();

        }

        // GET: api/Teams/5
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var team = _db.Teams
                .Include(x => x.Matches)
                .Include(x => x.Members)
                .AsEnumerable()
                .Select(Mapper.Map<TeamDetailsDTO>)
                .SingleOrDefault(x => x.Id == id);
            
            if (team == null) return NotFound();

            return Ok(team);
        }

        // GET: api/Teams/5/members
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}/members")]
        public IHttpActionResult TeamMembers(int id)
        {
            var team = _db.Teams
                .Include(x => x.Members)
                .SingleOrDefault(x => x.Id == id);
            
            if (team == null) return NotFound();

            return Ok(Mapper.Map<UserDTO>(team.Members));
        }

        // GET: api/Teams/5/tournaments
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("{id}/tournaments")]
        public IHttpActionResult TeamMatches(int id)
        {
            var team = _db.Teams
                .Include("Tournaments")
                .SingleOrDefault(x => x.Id == id);
            
            if (team == null) return NotFound();

            return Ok(Mapper.Map<TournamentDTO>(team.Tournaments));
        }

        // PUT: api/Teams/5
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Team(int id, TeamDTO team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != team.Id)
            {
                return BadRequest();
            }

            //_db.Entry(team).State = EntityState.Modified;
            var teamInDb = _db.Teams.Find(id); 

            if (teamInDb == null)
            {
                return NotFound();
            }

            teamInDb.Name = team.Name;
            teamInDb.Description = team.Description;
            teamInDb.Slogan = team.Slogan;
            teamInDb.Logo = team.Logo;
            teamInDb.Matches = team.MatchIds != null && team.MatchIds.Length > 0
                ? _db.Matches
                    .Where(x => team.MatchIds.Contains(x.Id))
                    .ToList()
                : new List<Match>();
            teamInDb.Members = team.MemberIds != null && team.MemberIds.Length > 0
                ? _db.Users
                    .Where(x => team.MemberIds.Contains(x.Id))
                    .ToList()
                : new List<ApplicationUser>();
            teamInDb.Tournaments = team.TournamentIds != null && team.TournamentIds.Length > 0
                ? _db.Tournaments
                    .Where(x => team.TournamentIds.Contains(x.Id))
                    .ToList()
                : new List<Tournament>();

            _db.SaveChanges();
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teams
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("")]
        public IHttpActionResult Team(TeamDTO team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamInDb = new Team
            {
                Name = team.Name,
                Description = team.Description,
                Slogan = team.Slogan,
                Logo = team.Logo,
                Matches = team.MatchIds != null || team.MatchIds.Length > 0
                    ? _db.Matches.Where(x => team.MatchIds.Contains(x.Id)).ToList()
                    : new List<Match>(),
                Members = team.MemberIds != null || team.MemberIds.Length > 0
                    ? _db.Users.Where(x => team.MemberIds.Contains(x.Id)).ToList()
                    : new List<ApplicationUser>(),
                Tournaments = team.TournamentIds != null || team.TournamentIds.Length > 0
                    ? _db.Tournaments.Where(x => team.TournamentIds.Contains(x.Id)).ToList()
                    : new List<Tournament>()
            };

            _db.Teams.Add(teamInDb);
            _db.SaveChanges();

            return CreatedAtRoute("", new { id = teamInDb.Id }, teamInDb);
        }

        // DELETE: api/Teams/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("{id}")]       
        public IHttpActionResult DeleteTeam(int id)
        {
            Team team = _db.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }

            _db.Teams.Remove(team);
            _db.SaveChanges();

            return Ok(team);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamExists(int id)
        {
            return _db.Teams.Count(e => e.Id == id) > 0;
        }
    }

    public class CommaDelimitedArrayModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
            if (val != null)
            {
                var s = val.AttemptedValue;
                if (s != null)
                {
                    try
                    {
                        var elementType = bindingContext.ModelType.GetElementType();
                        var converter = TypeDescriptor.GetConverter(elementType);
                        var values = Array.ConvertAll(s.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries),
                            x => { return converter.ConvertFromString(x != null ? x.Trim() : x); });

                        var typedValues = Array.CreateInstance(elementType, values.Length);

                        values.CopyTo(typedValues, 0);

                        bindingContext.Model = typedValues;
                    }
                    catch
                    {
                        bindingContext.Model = Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0);
                    }
                }
                else
                {
                    // change this line to null if you prefer nulls to empty arrays 
                    bindingContext.Model = Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0);
                }
                return true;
            }
            return false;
        }
    }
}