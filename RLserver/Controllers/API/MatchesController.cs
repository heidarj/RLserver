using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using AutoMapper;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.Controllers.API
{
    [RoutePrefix("api/Matches")]
    public class MatchesController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Matches and api/Matches?id=19,20
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get([ModelBinder(typeof(CommaDelimitedArrayModelBinder))]int[] id = null)
        {
            if (id == null)
            {
                var allMatches = _db.Matches
                    .Include("Tournament")
                    .Include("Teams")
                    .AsEnumerable()
                    .Select(Mapper.Map<MatchDTO>);
                
                return Ok(allMatches);
            }

            if (id.Length == 0) return BadRequest();

            var matches = _db.Matches
                .Include("Tournament")
                .Include("Teams")
                .Select(Mapper.Map<MatchDTO>)
                .Where(x => id.Contains(x.Id))
                .ToArray();
            if (matches.Any())
            {
                return Ok(matches);
            }

            return NotFound();
        }

        // GET: api/Matches/5
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(Match))]
        public IHttpActionResult GetMatch(int id)
        {
            var match = _db.Matches
                .Include("Tournament")
                .Include("Teams")
                .SingleOrDefault(x => x.Id == id);
            
            if (match == null) return NotFound();

            return Ok(match);
        }

        // PUT: api/Matches/5
        [Route("{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatch(int id, MatchDTO matchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matchDto.Id)
            {
                return BadRequest();
            }

            var matchInDb = _db.Matches.Find(id);
            
            if (matchInDb == null) return NotFound();

            matchInDb.Name = matchDto.Name;

            matchInDb.Teams = matchDto.TeamIds != null && matchDto.TeamIds.Any()
                ? _db.Teams.Where(x => matchDto.TeamIds.Contains(x.Id)).ToList()
                : new List<Team>();

            matchInDb.Tournament = _db.Tournaments.Find(matchDto.TournamentId);

            _db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Matches
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(MatchDTO))]
        public IHttpActionResult PostMatch(MatchDTO matchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMatch = new Match
            {
                Name = matchDto.Name,
                Teams = _db.Teams.Where(x => matchDto.TeamIds.Contains(x.Id)).ToList(),
                Tournament = _db.Tournaments.Find(matchDto.TournamentId)
            };

            _db.Matches.Add(newMatch);
            _db.SaveChanges();

            return CreatedAtRoute("", new { id = newMatch.Id }, newMatch);
        }

        // DELETE: api/Matches/5
        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(Match))]
        public IHttpActionResult DeleteMatch(int id)
        {
            var match = _db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            _db.Matches.Remove(match);
            _db.SaveChanges();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return _db.Matches.Count(e => e.Id == id) > 0;
        }
    }
}