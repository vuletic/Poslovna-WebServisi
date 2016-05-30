using System;
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
    public class MestoController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();
        private TokenHandler handler = new TokenHandler();

        // GET: api/Mesto
        [EnableQuery]
        public IQueryable<Mesto> GetMestoes()
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            return db.Mestoes;
        }

        // GET: api/Mesto/5
        [ResponseType(typeof(Mesto))]
        public async Task<IHttpActionResult> GetMesto(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Mesto mesto = await db.Mestoes.FindAsync(id);
            if (mesto == null)
            {
                return NotFound();
            }

            return Ok(mesto);
        }

        // PUT: api/Mesto/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMesto(decimal id, Mesto mesto)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mesto.Id)
            {
                return BadRequest();
            }

            db.Entry(mesto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MestoExists(id))
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

        // POST: api/Mesto
        [ResponseType(typeof(Mesto))]
        public async Task<IHttpActionResult> PostMesto(Mesto mesto)
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
                db.Mestoes.Add(mesto);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return null;
            }

            return CreatedAtRoute("DefaultApi", new { id = mesto.Id }, mesto);
        }

        // DELETE: api/Mesto/5
        [ResponseType(typeof(Mesto))]
        public async Task<IHttpActionResult> DeleteMesto(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return null;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return null;

            Mesto mesto = await db.Mestoes.FindAsync(id);
            if (mesto == null)
            {
                return NotFound();
            }

            db.Mestoes.Remove(mesto);
            await db.SaveChangesAsync();

            return Ok(mesto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MestoExists(decimal id)
        {
            return db.Mestoes.Count(e => e.Id == id) > 0;
        }
    }
}