using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RLserver.Models;
using RLserver.Controllers.API;

namespace RLserver.Controllers.API.Matches
{
    [RoutePrefix("api/Matches/{id}/Teams")]
    public class MatchesTeamsController : MyApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Matches/35/Teams
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var match = _db.Matches.Select(x => new {x.Id, x.Teams}).SingleOrDefault(x => x.Id == id);

            if (match == null) return NotFound();
            
            return Ok(match.Teams);
        }
        
        // POST: api/Matches/35/team
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(int id, List<Team> teams)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Teams = teams;

            return TrySaveDbChanges(_db, match);

        }

        // POST: api/Matches/35/team?teamId=
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(int id, [ModelBinder(typeof(CommaDelimitedArrayModelBinder))] int[] teamId)
        {
            // Check to see if there is a team with the supplied ID
            var teams = _db.Teams.Where(x => teamId.Contains(x.Id)).ToList();
            if (!teams.Any()) return BadRequest("teams with specified IDs not found.");

            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Teams = teams;

            return TrySaveDbChanges(_db, match);
        }






        // DELETE: api/Matches/35/team
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int id, Team team)
        {
            var match = _db.Matches.Find(id);

            if (match == null) return NotFound();

            match.Teams.Remove(team);


            return TrySaveDbChanges(_db, match);
        }
    }
}
