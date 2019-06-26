using Backend.Controllers;
using Backend.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using WebApiDepInject.Models;

namespace Backend
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            var corsAttr = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors(corsAttr);
            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();
            container.RegisterType<IRepository, Repository>();
            config.DependencyResolver = new UnityResolver(container);

            


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Services.Replace(typeof(ApiController),new AdsController(new Repository()));
        }
    }
}
