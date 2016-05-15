using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI;

namespace WebAPI.Controllers
{
    public class Stopa_PDVaController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Stopa_PDVa
        public IQueryable<Stopa_PDV_a> GetStopa_PDV_a()
        {
            return db.Stopa_PDV_a;
        }

        // GET: api/Stopa_PDVa/5
        [ResponseType(typeof(Stopa_PDV_a))]
        public async Task<IHttpActionResult> GetStopa_PDV_a(decimal id)
        {
            Stopa_PDV_a stopa_PDV_a = await db.Stopa_PDV_a.FindAsync(id);
            if (stopa_PDV_a == null)
            {
                return NotFound();
            }

            return Ok(stopa_PDV_a);
        }

        // PUT: api/Stopa_PDVa/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStopa_PDV_a(decimal id, Stopa_PDV_a stopa_PDV_a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stopa_PDV_a.Id_Stopa_PDV_a)
            {
                return BadRequest();
            }

            db.Entry(stopa_PDV_a).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Stopa_PDV_aExists(id))
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

        // POST: api/Stopa_PDVa
        [ResponseType(typeof(Stopa_PDV_a))]
        public async Task<IHttpActionResult> PostStopa_PDV_a(Stopa_PDV_a stopa_PDV_a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stopa_PDV_a.Add(stopa_PDV_a);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Stopa_PDV_aExists(stopa_PDV_a.Id_Stopa_PDV_a))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stopa_PDV_a.Id_Stopa_PDV_a }, stopa_PDV_a);
        }

        // DELETE: api/Stopa_PDVa/5
        [ResponseType(typeof(Stopa_PDV_a))]
        public async Task<IHttpActionResult> DeleteStopa_PDV_a(decimal id)
        {
            Stopa_PDV_a stopa_PDV_a = await db.Stopa_PDV_a.FindAsync(id);
            if (stopa_PDV_a == null)
            {
                return NotFound();
            }

            db.Stopa_PDV_a.Remove(stopa_PDV_a);
            await db.SaveChangesAsync();

            return Ok(stopa_PDV_a);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Stopa_PDV_aExists(decimal id)
        {
            return db.Stopa_PDV_a.Count(e => e.Id_Stopa_PDV_a == id) > 0;
        }
    }
}