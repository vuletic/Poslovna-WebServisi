using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;

namespace WebAPI
{
    public class TokenHandler
    {
        public bool CheckToken(string token)
        {
            try
            {
                string jsonPayload = JWT.JsonWebToken.Decode(token, Properties.Settings.Default.Secret);
            }
            catch (JWT.SignatureVerificationException)
            {
                return false;
            }
            return true;
        }
    }
}