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
    public class MagacinController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Magacin
        public IQueryable<Magacin> GetMagacins()
        {
            return db.Magacins;
        }

        // GET: api/Magacin/5
        [ResponseType(typeof(Magacin))]
        public async Task<IHttpActionResult> GetMagacin(decimal id)
        {
            Magacin magacin = await db.Magacins.FindAsync(id);
            if (magacin == null)
            {
                return NotFound();
            }

            return Ok(magacin);
        }

        // PUT: api/Magacin/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMagacin(decimal id, Magacin magacin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != magacin.Id_Magacin)
            {
                return BadRequest();
            }

            db.Entry(magacin).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MagacinExists(id))
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

        // POST: api/Magacin
        [ResponseType(typeof(Magacin))]
        public async Task<IHttpActionResult> PostMagacin(Magacin magacin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Magacins.Add(magacin);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MagacinExists(magacin.Id_Magacin))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = magacin.Id_Magacin }, magacin);
        }

        // DELETE: api/Magacin/5
        [ResponseType(typeof(Magacin))]
        public async Task<IHttpActionResult> DeleteMagacin(decimal id)
        {
            Magacin magacin = await db.Magacins.FindAsync(id);
            if (magacin == null)
            {
                return NotFound();
            }

            db.Magacins.Remove(magacin);
            await db.SaveChangesAsync();

            return Ok(magacin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MagacinExists(decimal id)
        {
            return db.Magacins.Count(e => e.Id_Magacin == id) > 0;
        }
    }
}