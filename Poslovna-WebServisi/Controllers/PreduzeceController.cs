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
    public class PreduzeceController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/Preduzece
        [EnableQuery]
        public IQueryable<Preduzece> GetPreduzeces()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            return db.Preduzeces;
        }

        // GET: api/Preduzece/5
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> GetPreduzece(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Preduzece preduzece = await db.Preduzeces.FindAsync(id);
            if (preduzece == null)
            {
                return NotFound();
            }

            return Ok(preduzece);
        }

        // PUT: api/Preduzece/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPreduzece(decimal id, Preduzece preduzece)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preduzece.Id_Preduzece)
            {
                return BadRequest();
            }

            db.Entry(preduzece).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreduzeceExists(id))
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

        // POST: api/Preduzece
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> PostPreduzece(Preduzece preduzece)
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
                db.Preduzeces.Add(preduzece);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return CreatedAtRoute("DefaultApi", new { id = preduzece.Id_Preduzece }, preduzece);
        }

        // DELETE: api/Preduzece/5
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> DeletePreduzece(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Preduzece preduzece = await db.Preduzeces.FindAsync(id);
            if (preduzece == null)
            {
                return NotFound();
            }

            try
            {
                db.Preduzeces.Remove(preduzece);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return Ok(preduzece);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreduzeceExists(decimal id)
        {
            return db.Preduzeces.Count(e => e.Id_Preduzece == id) > 0;
        }
    }
}