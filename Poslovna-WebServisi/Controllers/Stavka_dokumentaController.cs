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
    public class Stavka_dokumentaController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();
        // GET: api/Stavka_dokumenta
        [EnableQuery]
        public IQueryable<Stavka_dokumenta> GetStavka_dokumenta()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            return db.Stavka_dokumenta;
        }

        // GET: api/Stavka_dokumenta/5
        [ResponseType(typeof(Stavka_dokumenta))]
        public async Task<IHttpActionResult> GetStavka_dokumenta(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Stavka_dokumenta stavka_dokumenta = await db.Stavka_dokumenta.FindAsync(id);
            if (stavka_dokumenta == null)
            {
                return NotFound();
            }

            return Ok(stavka_dokumenta);
        }

        // PUT: api/Stavka_dokumenta/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStavka_dokumenta(decimal id, Stavka_dokumenta stavka_dokumenta)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stavka_dokumenta.Id_Stavka_dokumenta)
            {
                return BadRequest();
            }

            db.Entry(stavka_dokumenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stavka_dokumentaExists(id))
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

        // POST: api/Stavka_dokumenta
        [ResponseType(typeof(Stavka_dokumenta))]
        public async Task<IHttpActionResult> PostStavka_dokumenta(Stavka_dokumenta stavka_dokumenta)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stavka_dokumenta.Add(stavka_dokumenta);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = stavka_dokumenta.Id_Stavka_dokumenta }, stavka_dokumenta);
        }

        // DELETE: api/Stavka_dokumenta/5
        [ResponseType(typeof(Stavka_dokumenta))]
        public async Task<IHttpActionResult> DeleteStavka_dokumenta(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Stavka_dokumenta stavka_dokumenta = await db.Stavka_dokumenta.FindAsync(id);
            if (stavka_dokumenta == null)
            {
                return NotFound();
            }

            db.Stavka_dokumenta.Remove(stavka_dokumenta);
            await db.SaveChangesAsync();

            return Ok(stavka_dokumenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Stavka_dokumentaExists(decimal id)
        {
            return db.Stavka_dokumenta.Count(e => e.Id_Stavka_dokumenta == id) > 0;
        }
    }
}