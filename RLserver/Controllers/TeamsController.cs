using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Teams
        public ActionResult Index()
        {
            return View(_db.Teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = _db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamDTO team)
        {
            if (!ModelState.IsValid) return View(Mapper.Map<Team>(team));

            var teamInDb = new Team
            {
                Name = team.Name,
                Matches = team.MatchIds != null && team.MatchIds.Length > 0
                    ? _db.Matches.Where(x => team.MatchIds.Contains(x.Id)).ToList()
                    : new List<Match>(),
                Members = team.MemberIds != null && team.MemberIds.Length > 0
                    ? _db.Users.Where(x => team.MemberIds.Contains(x.Id)).ToList()
                    : new List<ApplicationUser>(),
                Tournaments = team.TournamentIds != null && team.TournamentIds.Length > 0
                    ? _db.Tournaments.Where(x => team.TournamentIds.Contains(x.Id)).ToList()
                    : new List<Tournament>()
            };

            _db.Teams.Add(teamInDb);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = teamInDb.Id});

        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = _db.Teams.Include(x => x.Members).Include(x => x.Tournaments).SingleOrDefault(x => x.Id == id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamDTO team)
        {
            if (!ModelState.IsValid) return View(Mapper.Map<Team>(team));

            var teamInDb = _db.Teams.Find(team.Id);

            if (teamInDb != null)
            {
                teamInDb.Name = team.Name;
                teamInDb.Matches = team.MatchIds != null && team.MatchIds.Length > 0
                    ? _db.Matches.Where(x => team.MatchIds.Contains(x.Id)).ToList()
                    : new List<Match>();
                teamInDb.Members = team.MemberIds != null && team.MemberIds.Length > 0
                    ? _db.Users.Where(x => team.MemberIds.Contains(x.Id)).ToList()
                    : new List<ApplicationUser>();
                teamInDb.Tournaments = team.MatchIds != null && team.TournamentIds.Length > 0
                    ? _db.Tournaments.Where(x => team.TournamentIds.Contains(x.Id)).ToList()
                    : new List<Tournament>();
            }

            //_db.Entry(team).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = team.Id});
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = _db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var team = _db.Teams.Find(id);
            
            _db.Teams.Remove(team);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}

