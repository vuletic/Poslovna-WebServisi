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
    public class PreduzeceController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Preduzece
        public IQueryable<Preduzece> GetPreduzeces()
        {
            return db.Preduzeces;
        }

        // GET: api/Preduzece/5
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> GetPreduzece(decimal id)
        {
            Preduzece preduzece = await db.Preduzeces.FindAsync(id);
            if (preduzece == null)
            {
                return NotFound();
            }

            return Ok(preduzece);
        }

        // PUT: api/Preduzece/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPreduzece(decimal id, Preduzece preduzece)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preduzece.Id_Preduzece)
            {
                return BadRequest();
            }

            db.Entry(preduzece).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreduzeceExists(id))
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

        // POST: api/Preduzece
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> PostPreduzece(Preduzece preduzece)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preduzeces.Add(preduzece);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PreduzeceExists(preduzece.Id_Preduzece))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = preduzece.Id_Preduzece }, preduzece);
        }

        // DELETE: api/Preduzece/5
        [ResponseType(typeof(Preduzece))]
        public async Task<IHttpActionResult> DeletePreduzece(decimal id)
        {
            Preduzece preduzece = await db.Preduzeces.FindAsync(id);
            if (preduzece == null)
            {
                return NotFound();
            }

            db.Preduzeces.Remove(preduzece);
            await db.SaveChangesAsync();

            return Ok(preduzece);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreduzeceExists(decimal id)
        {
            return db.Preduzeces.Count(e => e.Id_Preduzece == id) > 0;
        }
    }
}