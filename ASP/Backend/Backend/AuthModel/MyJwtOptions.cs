using Microsoft.Owin.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.AuthModel
{
    public class MyJwtOptions : JwtBearerAuthenticationOptions
    {
        public MyJwtOptions()
        {
            var issuer = "localhost";
            var audience = "all";
            var key = Convert.FromBase64String("this is my custom Secret key for authnetication");

            AllowedAudiences = new[] { audience };

            IssuerSecurityKeyProviders = new[]
            {
            new SymmetricKeyIssuerSecurityKeyProvider(issuer, key)
            };
        }
    }
}