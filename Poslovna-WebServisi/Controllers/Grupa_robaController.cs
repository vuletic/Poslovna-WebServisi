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
    public class Grupa_robaController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Grupa_roba
        public IQueryable<Grupa_roba> GetGrupa_roba()
        {
            return db.Grupa_roba;
        }

        // GET: api/Grupa_roba/5
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> GetGrupa_roba(decimal id)
        {
            Grupa_roba grupa_roba = await db.Grupa_roba.FindAsync(id);
            if (grupa_roba == null)
            {
                return NotFound();
            }

            return Ok(grupa_roba);
        }

        // PUT: api/Grupa_roba/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGrupa_roba(decimal id, Grupa_roba grupa_roba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupa_roba.Id_Grupa_roba)
            {
                return BadRequest();
            }

            db.Entry(grupa_roba).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Grupa_robaExists(id))
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

        // POST: api/Grupa_roba
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> PostGrupa_roba(Grupa_roba grupa_roba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupa_roba.Add(grupa_roba);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Grupa_robaExists(grupa_roba.Id_Grupa_roba))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grupa_roba.Id_Grupa_roba }, grupa_roba);
        }

        // DELETE: api/Grupa_roba/5
        [ResponseType(typeof(Grupa_roba))]
        public async Task<IHttpActionResult> DeleteGrupa_roba(decimal id)
        {
            Grupa_roba grupa_roba = await db.Grupa_roba.FindAsync(id);
            if (grupa_roba == null)
            {
                return NotFound();
            }

            db.Grupa_roba.Remove(grupa_roba);
            await db.SaveChangesAsync();

            return Ok(grupa_roba);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Grupa_robaExists(decimal id)
        {
            return db.Grupa_roba.Count(e => e.Id_Grupa_roba == id) > 0;
        }
    }
}