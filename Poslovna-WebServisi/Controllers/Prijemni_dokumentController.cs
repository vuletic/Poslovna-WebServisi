﻿using System;
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
    public class Prijemni_dokumentController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Prijemni_dokument
        public IQueryable<Prijemni_dokument> GetPrijemni_dokument()
        {
            return db.Prijemni_dokument;
        }

        // GET: api/Prijemni_dokument/5
        [ResponseType(typeof(Prijemni_dokument))]
        public async Task<IHttpActionResult> GetPrijemni_dokument(decimal id)
        {
            Prijemni_dokument prijemni_dokument = await db.Prijemni_dokument.FindAsync(id);
            if (prijemni_dokument == null)
            {
                return NotFound();
            }

            return Ok(prijemni_dokument);
        }

        // PUT: api/Prijemni_dokument/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrijemni_dokument(decimal id, Prijemni_dokument prijemni_dokument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prijemni_dokument.Id_Prijemni_dokument)
            {
                return BadRequest();
            }

            db.Entry(prijemni_dokument).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Prijemni_dokumentExists(id))
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

        // POST: api/Prijemni_dokument
        [ResponseType(typeof(Prijemni_dokument))]
        public async Task<IHttpActionResult> PostPrijemni_dokument(Prijemni_dokument prijemni_dokument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prijemni_dokument.Add(prijemni_dokument);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Prijemni_dokumentExists(prijemni_dokument.Id_Prijemni_dokument))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = prijemni_dokument.Id_Prijemni_dokument }, prijemni_dokument);
        }

        // DELETE: api/Prijemni_dokument/5
        [ResponseType(typeof(Prijemni_dokument))]
        public async Task<IHttpActionResult> DeletePrijemni_dokument(decimal id)
        {
            Prijemni_dokument prijemni_dokument = await db.Prijemni_dokument.FindAsync(id);
            if (prijemni_dokument == null)
            {
                return NotFound();
            }

            db.Prijemni_dokument.Remove(prijemni_dokument);
            await db.SaveChangesAsync();

            return Ok(prijemni_dokument);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Prijemni_dokumentExists(decimal id)
        {
            return db.Prijemni_dokument.Count(e => e.Id_Prijemni_dokument == id) > 0;
        }
    }
}