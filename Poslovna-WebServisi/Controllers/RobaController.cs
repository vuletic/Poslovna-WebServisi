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
    public class RobaController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Roba
        public IQueryable<Roba> GetRobas()
        {
            return db.Robas;
        }

        // GET: api/Roba/5
        [ResponseType(typeof(Roba))]
        public async Task<IHttpActionResult> GetRoba(decimal id)
        {
            Roba roba = await db.Robas.FindAsync(id);
            if (roba == null)
            {
                return NotFound();
            }

            return Ok(roba);
        }

        // PUT: api/Roba/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoba(decimal id, Roba roba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roba.Id_Roba)
            {
                return BadRequest();
            }

            db.Entry(roba).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RobaExists(id))
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

        // POST: api/Roba
        [ResponseType(typeof(Roba))]
        public async Task<IHttpActionResult> PostRoba(Roba roba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Robas.Add(roba);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = roba.Id_Roba }, roba);
        }

        // DELETE: api/Roba/5
        [ResponseType(typeof(Roba))]
        public async Task<IHttpActionResult> DeleteRoba(decimal id)
        {
            Roba roba = await db.Robas.FindAsync(id);
            if (roba == null)
            {
                return NotFound();
            }

            db.Robas.Remove(roba);
            await db.SaveChangesAsync();

            return Ok(roba);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RobaExists(decimal id)
        {
            return db.Robas.Count(e => e.Id_Roba == id) > 0;
        }
    }
}