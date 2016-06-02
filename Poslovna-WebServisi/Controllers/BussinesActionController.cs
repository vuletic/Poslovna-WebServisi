using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class BussinesActionController : ApiController
    {

        private PoslovnaEntities db = new PoslovnaEntities();

        // GET: api/BussinesAction
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BussinesAction/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BussinesAction
        public void Post(BussinesAction action)
        {
            db.Proknjizi(15, false);
        }

        // PUT: api/BussinesAction/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BussinesAction/5
        public void Delete(int id)
        {
        }

        public class BussinesAction
        {
            public string action { get; set; }
            public decimal id { get; set; } 
        }
    }
}
