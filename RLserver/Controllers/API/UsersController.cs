using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RLserver.Models;
using RLserver.Models.DTOs;

namespace RLserver.Controllers.API
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly UserManager<ApplicationUser> _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        // GET: api/Users and api/Users?id=19,20
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get([ModelBinder(typeof(CommaDelimitedArrayModelBinder))]string[] id = null)
        {
            if (id == null)
            {
                var all = _manager.Users
                                    .Include(x => x.Teams)
                                    .AsEnumerable()
                                    .Select(
                                        Mapper.Map<ApplicationUser, UserDTO>
                                    ); 
                
                return Ok(all);
            }
            else
            {
                var users = _manager.Users
                                        .Include(x => x.Teams)
                                        .Where(x => id.Contains(x.Id))
                                        .AsEnumerable()
                                        .Select(
                                            Mapper.Map<ApplicationUser, UserDTO>
                                        );
                
                return Ok(users);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _manager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
