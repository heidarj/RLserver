using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));


        // GET: Users
        public ActionResult Index()
        {
            var users = _manager.Users.Include(x => x.Teams).AsEnumerable().Select(Mapper.Map<ApplicationUser, UserDTO>);
            return View(users);
        }


        // GET: Details
        public ActionResult Details(string id)
        {
            var user = _manager.Users.Include("Teams").SingleOrDefault(x => x.Id == id || x.UserName == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<UserDTO>(user));
        }

        
        // GET: Users/UserSelector
        public ActionResult UserSelector()
        {
            var users = _manager.Users.AsEnumerable().Select(Mapper.Map<ApplicationUser, UserDTO>);
            return PartialView("_SelectUser", users);
        }
    }
}