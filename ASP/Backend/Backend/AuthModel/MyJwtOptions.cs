using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
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
            var key = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");
            AuthenticationMode = AuthenticationMode.Active;
            AllowedAudiences = new[] { audience };

            IssuerSecurityKeyProviders = new[]
            {
            new SymmetricKeyIssuerSecurityKeyProvider(issuer, key)
            };
        }
    }
}