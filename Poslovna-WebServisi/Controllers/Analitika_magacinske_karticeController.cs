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
    public class Analitika_magacinske_karticeController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/Analitika_magacinske_kartice
        [EnableQuery]
        public IQueryable<Analitika_magacinske_kartice> GetAnalitika_magacinske_kartice()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            return db.Analitika_magacinske_kartice.Include(amg => amg.Robna_kartica.Magacin).Include(amg => amg.Robna_kartica.Roba);
        }

        // GET: api/Analitika_magacinske_kartice/5
        [ResponseType(typeof(Analitika_magacinske_kartice))]
        public async Task<IHttpActionResult> GetAnalitika_magacinske_kartice(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Analitika_magacinske_kartice analitika_magacinske_kartice = await db.Analitika_magacinske_kartice.FindAsync(id);
            if (analitika_magacinske_kartice == null)
            {
                return NotFound();
            }

            return Ok(analitika_magacinske_kartice);
        }

        // PUT: api/Analitika_magacinske_kartice/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnalitika_magacinske_kartice(decimal id, Analitika_magacinske_kartice analitika_magacinske_kartice)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != analitika_magacinske_kartice.Id_Analitika_magacinske_kartice)
            {
                return BadRequest();
            }

            db.Entry(analitika_magacinske_kartice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Analitika_magacinske_karticeExists(id))
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

        // POST: api/Analitika_magacinske_kartice
        [ResponseType(typeof(Analitika_magacinske_kartice))]
        public async Task<IHttpActionResult> PostAnalitika_magacinske_kartice(Analitika_magacinske_kartice analitika_magacinske_kartice)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Analitika_magacinske_kartice.Add(analitika_magacinske_kartice);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = analitika_magacinske_kartice.Id_Analitika_magacinske_kartice }, analitika_magacinske_kartice);
        }

        // DELETE: api/Analitika_magacinske_kartice/5
        [ResponseType(typeof(Analitika_magacinske_kartice))]
        public async Task<IHttpActionResult> DeleteAnalitika_magacinske_kartice(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Analitika_magacinske_kartice analitika_magacinske_kartice = await db.Analitika_magacinske_kartice.FindAsync(id);
            if (analitika_magacinske_kartice == null)
            {
                return NotFound();
            }

            db.Analitika_magacinske_kartice.Remove(analitika_magacinske_kartice);
            await db.SaveChangesAsync();

            return Ok(analitika_magacinske_kartice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Analitika_magacinske_karticeExists(decimal id)
        {
            return db.Analitika_magacinske_kartice.Count(e => e.Id_Analitika_magacinske_kartice == id) > 0;
        }
    }
}