using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Web.Http;
using RLserver.Models;

namespace RLserver.Controllers.API
{
    public class MyApiController : ApiController
    {
        public IHttpActionResult TrySaveDbChanges(ApplicationDbContext _db, object returnObject)
        {
            try
            {
                _db.SaveChangesAsync();

                return Ok(returnObject);
            }
            catch (DbUpdateException e)
            {
                return InternalServerError(e);
            }
            catch (DbEntityValidationException e)
            {
                return BadRequest(e.Message);
            }
             catch
            {
                return InternalServerError();
            }
        }
    }
}