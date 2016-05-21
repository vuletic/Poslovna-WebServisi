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
    public class Jedinica_mereController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Jedinica_mere
        public IQueryable<Jedinica_mere> GetJedinica_mere()
        {
            return db.Jedinica_mere;
        }

        // GET: api/Jedinica_mere/5
        [ResponseType(typeof(Jedinica_mere))]
        public async Task<IHttpActionResult> GetJedinica_mere(decimal id)
        {
            Jedinica_mere jedinica_mere = await db.Jedinica_mere.FindAsync(id);
            if (jedinica_mere == null)
            {
                return NotFound();
            }

            return Ok(jedinica_mere);
        }

        // PUT: api/Jedinica_mere/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJedinica_mere(decimal id, Jedinica_mere jedinica_mere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jedinica_mere.Id_Jedinica_mere)
            {
                return BadRequest();
            }

            db.Entry(jedinica_mere).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Jedinica_mereExists(id))
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

        // POST: api/Jedinica_mere
        [ResponseType(typeof(Jedinica_mere))]
        public async Task<IHttpActionResult> PostJedinica_mere(Jedinica_mere jedinica_mere)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jedinica_mere.Add(jedinica_mere);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = jedinica_mere.Id_Jedinica_mere }, jedinica_mere);
        }

        // DELETE: api/Jedinica_mere/5
        [ResponseType(typeof(Jedinica_mere))]
        public async Task<IHttpActionResult> DeleteJedinica_mere(decimal id)
        {
            Jedinica_mere jedinica_mere = await db.Jedinica_mere.FindAsync(id);
            if (jedinica_mere == null)
            {
                return NotFound();
            }

            db.Jedinica_mere.Remove(jedinica_mere);
            await db.SaveChangesAsync();

            return Ok(jedinica_mere);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Jedinica_mereExists(decimal id)
        {
            return db.Jedinica_mere.Count(e => e.Id_Jedinica_mere == id) > 0;
        }
    }
}