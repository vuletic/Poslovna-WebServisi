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
    public class PDVController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/PDV
        [EnableQuery]
        public IQueryable<PDV> GetPDVs()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            return db.PDVs;
        }

        // GET: api/PDV/5
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> GetPDV(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            PDV pDV = await db.PDVs.FindAsync(id);
            if (pDV == null)
            {
                return NotFound();
            }

            return Ok(pDV);
        }

        // PUT: api/PDV/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPDV(decimal id, PDV pDV)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pDV.Id_PDV)
            {
                return BadRequest();
            }

            db.Entry(pDV).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PDVExists(id))
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

        // POST: api/PDV
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> PostPDV(PDV pDV)
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
                db.PDVs.Add(pDV);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return CreatedAtRoute("DefaultApi", new { id = pDV.Id_PDV }, pDV);
        }

        // DELETE: api/PDV/5
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> DeletePDV(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            PDV pDV = await db.PDVs.FindAsync(id);
            if (pDV == null)
            {
                return NotFound();
            }

            try
            {
                db.PDVs.Remove(pDV);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return Ok(pDV);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PDVExists(decimal id)
        {
            return db.PDVs.Count(e => e.Id_PDV == id) > 0;
        }
    }
}