using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RLserver.Models;

namespace RLserver.Controllers.API.Matches
{
    [RoutePrefix("api/Matches/{id}/Tournament")]
    public class MatchesTournamentController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Matches/35/Tournament
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var match = _db.Matches.Select(x => new {x.Id, x.Tournament}).SingleOrDefault(x => x.Id == id);

            if (match == null) return NotFound();
            
            return Ok(match.Tournament);
        }
        
        // POST: api/Matches/35/Tournament
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(int id, Tournament tournament)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Tournament = tournament;

            _db.SaveChanges();

            return Ok(match);
        }

        // POST: api/Matches/35/Tournament/18
        [Route("{tournamentId}")]
        [HttpPost]
        public IHttpActionResult Post(int id, int tournamentId)
        {
            // Check to see if there is a tournament with the supplied ID
            var tournament = _db.Tournaments.Find(tournamentId);
            if (tournament == null) return BadRequest("Tournament with specified ID not found.");

            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Tournament = tournament;

            _db.SaveChanges();

            return Ok(match);
        }

        // DELETE: api/Matches/35/Tournament
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Tournament = null;

            _db.SaveChangesAsync();

            return Ok();
        }
    }
}
