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
    public class Robna_karticaController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Robna_kartica
        public IQueryable<Robna_kartica> GetRobna_kartica()
        {
            return db.Robna_kartica;
        }

        // GET: api/Robna_kartica/5
        [ResponseType(typeof(Robna_kartica))]
        public async Task<IHttpActionResult> GetRobna_kartica(decimal id)
        {
            Robna_kartica robna_kartica = await db.Robna_kartica.FindAsync(id);
            if (robna_kartica == null)
            {
                return NotFound();
            }

            return Ok(robna_kartica);
        }

        // PUT: api/Robna_kartica/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRobna_kartica(decimal id, Robna_kartica robna_kartica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != robna_kartica.Id_Robna_kartica)
            {
                return BadRequest();
            }

            db.Entry(robna_kartica).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Robna_karticaExists(id))
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

        // POST: api/Robna_kartica
        [ResponseType(typeof(Robna_kartica))]
        public async Task<IHttpActionResult> PostRobna_kartica(Robna_kartica robna_kartica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Robna_kartica.Add(robna_kartica);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = robna_kartica.Id_Robna_kartica }, robna_kartica);
        }

        // DELETE: api/Robna_kartica/5
        [ResponseType(typeof(Robna_kartica))]
        public async Task<IHttpActionResult> DeleteRobna_kartica(decimal id)
        {
            Robna_kartica robna_kartica = await db.Robna_kartica.FindAsync(id);
            if (robna_kartica == null)
            {
                return NotFound();
            }

            db.Robna_kartica.Remove(robna_kartica);
            await db.SaveChangesAsync();

            return Ok(robna_kartica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Robna_karticaExists(decimal id)
        {
            return db.Robna_kartica.Count(e => e.Id_Robna_kartica == id) > 0;
        }
    }
}