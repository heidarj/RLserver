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
using System.Web.Http.ModelBinding;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.Controllers.API
{
    [RoutePrefix("api/teams")]
    public class TeamsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Teams and api/Teams?id=19,20
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get([ModelBinder(typeof(CommaDelimitedArrayModelBinder))]int[] id = null)
        {
            if (id == null)
            {
                var allTeams = _db.Teams.Include(x => x.Members); 
                return Ok(allTeams);
            }
            else
            {
                var teams = _db.Teams.Include(x => x.Members).Where(x => id.Contains(x.Id));
                return Ok(teams);
            }
        }

        // GET: api/Teams/5
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Team team = _db.Teams.Include(x => x.Matches).Include(x => x.Members).SingleOrDefault(x => x.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // GET: api/Teams/5/members
        [HttpGet]
        [Route("{id}/members")]
        public IHttpActionResult TeamMembers(int id)
        {
            var team = _db.Teams.Include(x => x.Members).SingleOrDefault(x => x.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team.Members);
        }

        // GET: api/Teams/5/tournaments
        [HttpGet]
        [Route("{id}/tournaments")]
        public IHttpActionResult TeamMatches(int id)
        {
            var team = _db.Teams.Include(x => x.Tournaments).SingleOrDefault(x => x.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok(team.Matches);
        }

        // PUT: api/Teams/5
        [HttpPut]
        [Route("{id}")]
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
            teamInDb.Matches = team.Matches != null || team.Matches.Length > 0
                ? _db.Matches.Where(x => team.Matches.Contains(x.Id)).ToList()
                : new List<Match>();
            teamInDb.Members = team.Members != null || team.Members.Length > 0
                ? _db.Users.Where(x => team.Members.Contains(x.Id)).ToList()
                : new List<ApplicationUser>();
            teamInDb.Tournaments = team.Tournaments != null || team.Tournaments.Length > 0
                ? _db.Tournaments.Where(x => team.Tournaments.Contains(x.Id)).ToList()
                : new List<Tournament>();

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Teams
        [HttpPost]
        [Route("")]
        public IHttpActionResult Team(TeamDTO team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamInDb = new Team
            {
                Name = team.Name,
                Matches = team.Matches != null || team.Matches.Length > 0
                    ? _db.Matches.Where(x => team.Matches.Contains(x.Id)).ToList()
                    : new List<Match>(),
                Members = team.Members != null || team.Members.Length > 0
                    ? _db.Users.Where(x => team.Members.Contains(x.Id)).ToList()
                    : new List<ApplicationUser>(),
                Tournaments = team.Tournaments != null || team.Tournaments.Length > 0
                    ? _db.Tournaments.Where(x => team.Tournaments.Contains(x.Id)).ToList()
                    : new List<Tournament>()
            };

            _db.Teams.Add(teamInDb);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = team.Id }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete]
        [Route("{id}")]       
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
                    var elementType = bindingContext.ModelType.GetElementType();
                    var converter = TypeDescriptor.GetConverter(elementType);
                    var values = Array.ConvertAll(s.Split(new[] { ","},StringSplitOptions.RemoveEmptyEntries),
                        x => { return converter.ConvertFromString(x != null ? x.Trim() : x); });

                    var typedValues = Array.CreateInstance(elementType, values.Length);

                    values.CopyTo(typedValues, 0);

                    bindingContext.Model = typedValues;
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