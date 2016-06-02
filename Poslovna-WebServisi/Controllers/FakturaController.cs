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
    public class FakturaController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/Faktura
        [EnableQuery]
        public IQueryable<Faktura> GetFakturas()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            return db.Fakturas.Include(f => f.Poslovna_godina).Include(f => f.Poslovni_partner);
        }

        // GET: api/Faktura/5
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> GetFaktura(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Faktura faktura = await db.Fakturas.FindAsync(id);
            if (faktura == null)
            {
                return NotFound();
            }

            return Ok(faktura);
        }

        // PUT: api/Faktura/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFaktura(decimal id, Faktura faktura)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faktura.Id_Faktura)
            {
                return BadRequest();
            }

            db.Entry(faktura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FakturaExists(id))
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

        // POST: api/Faktura
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> PostFaktura(Faktura faktura)
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
                db.Fakturas.Add(faktura);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }
            return CreatedAtRoute("DefaultApi", new { id = faktura.Id_Faktura }, faktura);
        }

        // DELETE: api/Faktura/5
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> DeleteFaktura(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;
            Faktura faktura = await db.Fakturas.FindAsync(id);
            if (faktura == null)
            {
                return NotFound();
            }

            db.Fakturas.Remove(faktura);
            await db.SaveChangesAsync();

            return Ok(faktura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FakturaExists(decimal id)
        {
            return db.Fakturas.Count(e => e.Id_Faktura == id) > 0;
        }
    }
}