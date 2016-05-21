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
    public class Poslovni_partnerController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Poslovni_partner
        public IQueryable<Poslovni_partner> GetPoslovni_partner()
        {
            return db.Poslovni_partner;
        }

        // GET: api/Poslovni_partner/5
        [ResponseType(typeof(Poslovni_partner))]
        public async Task<IHttpActionResult> GetPoslovni_partner(decimal id)
        {
            Poslovni_partner poslovni_partner = await db.Poslovni_partner.FindAsync(id);
            if (poslovni_partner == null)
            {
                return NotFound();
            }

            return Ok(poslovni_partner);
        }

        // PUT: api/Poslovni_partner/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoslovni_partner(decimal id, Poslovni_partner poslovni_partner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poslovni_partner.Id_Partner)
            {
                return BadRequest();
            }

            db.Entry(poslovni_partner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Poslovni_partnerExists(id))
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

        // POST: api/Poslovni_partner
        [ResponseType(typeof(Poslovni_partner))]
        public async Task<IHttpActionResult> PostPoslovni_partner(Poslovni_partner poslovni_partner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Poslovni_partner.Add(poslovni_partner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = poslovni_partner.Id_Partner }, poslovni_partner);
        }

        // DELETE: api/Poslovni_partner/5
        [ResponseType(typeof(Poslovni_partner))]
        public async Task<IHttpActionResult> DeletePoslovni_partner(decimal id)
        {
            Poslovni_partner poslovni_partner = await db.Poslovni_partner.FindAsync(id);
            if (poslovni_partner == null)
            {
                return NotFound();
            }

            db.Poslovni_partner.Remove(poslovni_partner);
            await db.SaveChangesAsync();

            return Ok(poslovni_partner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Poslovni_partnerExists(decimal id)
        {
            return db.Poslovni_partner.Count(e => e.Id_Partner == id) > 0;
        }
    }
}