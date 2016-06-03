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
        private TokenHandler handler = new TokenHandler();

        [Route("api/calculate/{id}")]
        [HttpPost]
        public bool Calculate(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Iskalkulisi(id);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [Route("api/record/{id}")]
        [HttpPost]
        public bool Record(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Proknjizi(id, false);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [Route("api/cancel/{id}")]
        [HttpPost]
        public bool Cancel(decimal id)
        {
            if (Request.Headers.Authorization == null)
                return false;

            if (!handler.CheckToken(Request.Headers.Authorization.ToString()))
                return false;
            try
            {
                db.Proknjizi(id, true);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
