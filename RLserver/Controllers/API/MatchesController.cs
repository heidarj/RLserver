using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using RLserver.Models;

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
                var allMatches = _db.Matches; 
                return Ok(allMatches);
            }
            else
            {
                var matches = _db.Matches.Where(x => id.Contains(x.Id));
                return Ok(matches);
            }
        }

        // GET: api/Matches/5
        [ResponseType(typeof(Match))]
        public IHttpActionResult GetMatch(int id)
        {
            Match match = _db.Matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/Matches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMatch(int id, Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.Id)
            {
                return BadRequest();
            }

            _db.Entry(match).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        [ResponseType(typeof(Match))]
        public IHttpActionResult PostMatch(Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Matches.Add(match);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = match.Id }, match);
        }

        // DELETE: api/Matches/5
        [ResponseType(typeof(Match))]
        public IHttpActionResult DeleteMatch(int id)
        {
            Match match = _db.Matches.Find(id);
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