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
    public class PDVController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/PDV
        public IQueryable<PDV> GetPDVs()
        {
            return db.PDVs;
        }

        // GET: api/PDV/5
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> GetPDV(decimal id)
        {
            PDV pDV = await db.PDVs.FindAsync(id);
            if (pDV == null)
            {
                return NotFound();
            }

            return Ok(pDV);
        }

        // PUT: api/PDV/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPDV(decimal id, PDV pDV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pDV.Id_PDV)
            {
                return BadRequest();
            }

            db.Entry(pDV).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PDVExists(id))
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

        // POST: api/PDV
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> PostPDV(PDV pDV)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PDVs.Add(pDV);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pDV.Id_PDV }, pDV);
        }

        // DELETE: api/PDV/5
        [ResponseType(typeof(PDV))]
        public async Task<IHttpActionResult> DeletePDV(decimal id)
        {
            PDV pDV = await db.PDVs.FindAsync(id);
            if (pDV == null)
            {
                return NotFound();
            }

            db.PDVs.Remove(pDV);
            await db.SaveChangesAsync();

            return Ok(pDV);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PDVExists(decimal id)
        {
            return db.PDVs.Count(e => e.Id_PDV == id) > 0;
        }
    }
}