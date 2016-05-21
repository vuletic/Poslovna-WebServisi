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
    public class FakturaController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Faktura
        public IQueryable<Faktura> GetFakturas()
        {
            return db.Fakturas;
        }

        // GET: api/Faktura/5
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> GetFaktura(decimal id)
        {
            Faktura faktura = await db.Fakturas.FindAsync(id);
            if (faktura == null)
            {
                return NotFound();
            }

            return Ok(faktura);
        }

        // PUT: api/Faktura/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFaktura(decimal id, Faktura faktura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faktura.Id_Faktura)
            {
                return BadRequest();
            }

            db.Entry(faktura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FakturaExists(id))
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

        // POST: api/Faktura
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> PostFaktura(Faktura faktura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fakturas.Add(faktura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = faktura.Id_Faktura }, faktura);
        }

        // DELETE: api/Faktura/5
        [ResponseType(typeof(Faktura))]
        public async Task<IHttpActionResult> DeleteFaktura(decimal id)
        {
            Faktura faktura = await db.Fakturas.FindAsync(id);
            if (faktura == null)
            {
                return NotFound();
            }

            db.Fakturas.Remove(faktura);
            await db.SaveChangesAsync();

            return Ok(faktura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FakturaExists(decimal id)
        {
            return db.Fakturas.Count(e => e.Id_Faktura == id) > 0;
        }
    }
}