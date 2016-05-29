﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace WebAPI.Controllers
{
    public class KorisnikController : ApiController
    {
        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/Korisnik
        [EnableQuery]
        public IQueryable<Korisnik> GetKorisniks()
        {
            return db.Korisniks;
        }

        // GET: api/Korisnik/5
        [ResponseType(typeof(Korisnik))]
        public async Task<IHttpActionResult> GetKorisnik(decimal id)
        {
            Korisnik korisnik = await db.Korisniks.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return Ok(korisnik);
        }

        // PUT: api/Korisnik/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutKorisnik(decimal id, Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != korisnik.Id_Korisnik)
            {
                return BadRequest();
            }

            db.Entry(korisnik).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
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

        // POST: api/Korisnik
        [ResponseType(typeof(String))]
        public String PostKorisnik(Korisnik korisnik)
        {
           
            Korisnik k = (Korisnik)(from kor in db.Korisniks
                                   where kor.Korisnicko_ime_Korisnik == korisnik.Korisnicko_ime_Korisnik
                                   && kor.Lozinka_Korisnik == korisnik.Lozinka_Korisnik
                                   select kor).FirstOrDefault();

            string jwt = "";

            string secret = "Test";
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(secret);
            string encodedText = Convert.ToBase64String(bytesToEncode);

            var payload = new Dictionary<string, object>()
            {
                { "sub", k.Korisnicko_ime_Korisnik } 
            };
       
            jwt = JWT.JsonWebToken.Encode(payload, secret, JWT.JwtHashAlgorithm.HS256);

            if (k != null)
                return jwt;
            else
                return null;
        }


        // DELETE: api/Korisnik/5
        [ResponseType(typeof(Korisnik))]
        public async Task<IHttpActionResult> DeleteKorisnik(decimal id)
        {
            Korisnik korisnik = await db.Korisniks.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            db.Korisniks.Remove(korisnik);
            await db.SaveChangesAsync();

            return Ok(korisnik);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KorisnikExists(decimal id)
        {
            return db.Korisniks.Count(e => e.Id_Korisnik == id) > 0;
        }
    }
}