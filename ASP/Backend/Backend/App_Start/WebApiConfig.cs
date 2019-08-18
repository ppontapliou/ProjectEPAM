using Backend.Interfaces;
using Backend.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Unity;

namespace Backend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;


            var container = new UnityContainer();
            container.RegisterType(typeof(IRepository), typeof(Repository));
            
            config.DependencyResolver = new UnityResolver(container);
            container.RegisterType(typeof(ICacheHandler), typeof(CacheHandler));
            //var container1 = new UnityContainer();
            //container1.RegisterType(typeof(ICacheHandler), typeof(CacheHandler));
        }
    }
}
