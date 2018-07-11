using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RLserver.Models;

namespace RLserver.Controllers.API
{
    [RoutePrefix("api/tournaments")]
    public class TournamentsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Tournaments and api/Tournaments?id=19,20
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get([ModelBinder(typeof(CommaDelimitedArrayModelBinder))]int[] id = null)
        {
            if (id == null)
            {
                var all = _db.Tournaments.Include(x => x.Matches).Include(x => x.Teams); 
                return Ok(all);
            }
            else
            {
                var tournaments = _db.Tournaments.Include(x => x.Matches).Include(x => x.Teams).Where(x => id.Contains(x.Id));
                return Ok(tournaments);
            }
        }

        // GET: api/Tournaments/5
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetTournament(int id)
        {
            var tournament = _db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        // PUT: api/Tournaments/5
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult PutTournament(int id, Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tournament.Id)
            {
                return BadRequest();
            }

            _db.Entry(tournament).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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

        // POST: api/Tournaments
        [Route("")]
        [HttpPost]
        public IHttpActionResult PostTournament(Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Tournaments.Add(tournament);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments/5
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteTournament(int id)
        {
            var tournament = _db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _db.Tournaments.Remove(tournament);
            _db.SaveChanges();

            return Ok(tournament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TournamentExists(int id)
        {
            return _db.Tournaments.Count(e => e.Id == id) > 0;
        }
    }
}