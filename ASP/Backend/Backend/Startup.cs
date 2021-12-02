using Backend.AuthModel;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Backend.Startup))]
namespace Backend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            config.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, PUT, DELETE"));
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            var timeSpan = TimeSpan.FromDays(1);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = timeSpan,
                Provider = new MyOAuthProvider(),
                AccessTokenFormat = new MyJwtFormat(new OAuthAuthorizationServerOptions() { AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30) }),
                RefreshTokenProvider = new RefreshTokenProvider(),
                RefreshTokenFormat = new MyJwtFormat(new OAuthAuthorizationServerOptions() { AccessTokenExpireTimeSpan = timeSpan }),
            };

            // Token Generation
            app.UseOAuthBearerTokens(OAuthServerOptions);
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseJwtBearerAuthentication(new MyJwtOptions());
        }
    }
}