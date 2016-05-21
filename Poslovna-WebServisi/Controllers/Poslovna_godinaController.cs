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
    public class Poslovna_godinaController : ApiController
    {
        private Poslovna db = new Poslovna();

        // GET: api/Poslovna_godina
        public IQueryable<Poslovna_godina> GetPoslovna_godina()
        {
            return db.Poslovna_godina;
        }

        // GET: api/Poslovna_godina/5
        [ResponseType(typeof(Poslovna_godina))]
        public async Task<IHttpActionResult> GetPoslovna_godina(decimal id)
        {
            Poslovna_godina poslovna_godina = await db.Poslovna_godina.FindAsync(id);
            if (poslovna_godina == null)
            {
                return NotFound();
            }

            return Ok(poslovna_godina);
        }

        // PUT: api/Poslovna_godina/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoslovna_godina(decimal id, Poslovna_godina poslovna_godina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poslovna_godina.Id_Poslovna_godina)
            {
                return BadRequest();
            }

            db.Entry(poslovna_godina).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Poslovna_godinaExists(id))
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

        // POST: api/Poslovna_godina
        [ResponseType(typeof(Poslovna_godina))]
        public async Task<IHttpActionResult> PostPoslovna_godina(Poslovna_godina poslovna_godina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Poslovna_godina.Add(poslovna_godina);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = poslovna_godina.Id_Poslovna_godina }, poslovna_godina);
        }

        // DELETE: api/Poslovna_godina/5
        [ResponseType(typeof(Poslovna_godina))]
        public async Task<IHttpActionResult> DeletePoslovna_godina(decimal id)
        {
            Poslovna_godina poslovna_godina = await db.Poslovna_godina.FindAsync(id);
            if (poslovna_godina == null)
            {
                return NotFound();
            }

            db.Poslovna_godina.Remove(poslovna_godina);
            await db.SaveChangesAsync();

            return Ok(poslovna_godina);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Poslovna_godinaExists(decimal id)
        {
            return db.Poslovna_godina.Count(e => e.Id_Poslovna_godina == id) > 0;
        }
    }
}