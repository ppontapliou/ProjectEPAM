using Backend.AuthModel;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Backend.Startup))]
namespace Backend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            var timeSpan = TimeSpan.FromHours(1);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = timeSpan,
                Provider = new MyOAuthProvider(),
                AccessTokenFormat = new MyJwtFormat(new OAuthAuthorizationServerOptions() { AccessTokenExpireTimeSpan = timeSpan })

            };

            // Token Generation

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseJwtBearerAuthentication(new MyJwtOptions());


        }
    }
}