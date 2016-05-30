using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace WebAPI.Controllers
{
    public class Grupa_robaController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/Grupa_roba
        [EnableQuery]
        public IQueryable<Grupa_roba> GetGrupa_roba()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            return db.Grupa_roba;
        }

        // GET: api/Grupa_roba/5
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> GetGrupa_roba(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Grupa_roba grupa_roba = await db.Grupa_roba.FindAsync(id);
            if (grupa_roba == null)
            {
                return NotFound();
            }

            return Ok(grupa_roba);
        }

        // PUT: api/Grupa_roba/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrupa_roba(decimal id, Grupa_roba grupa_roba)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupa_roba.Id_Grupa_roba)
            {
                return BadRequest();
            }

            db.Entry(grupa_roba).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Grupa_robaExists(id))
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

        // POST: api/Grupa_roba
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> PostGrupa_roba(Grupa_roba grupa_roba)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.Grupa_roba.Add(grupa_roba);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return CreatedAtRoute("DefaultApi", new { id = grupa_roba.Id_Grupa_roba }, grupa_roba);
        }

        // DELETE: api/Grupa_roba/5
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> DeleteGrupa_roba(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Grupa_roba grupa_roba = await db.Grupa_roba.FindAsync(id);
            if (grupa_roba == null)
            {
                return NotFound();
            }

            db.Grupa_roba.Remove(grupa_roba);
            await db.SaveChangesAsync();

            return Ok(grupa_roba);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Grupa_robaExists(decimal id)
        {
            return db.Grupa_roba.Count(e => e.Id_Grupa_roba == id) > 0;
        }
    }
}