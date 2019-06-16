using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using StructureMap;
using Backend.Interfaces;

namespace Backend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       

        protected void Application_Start()
        {            
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var Container = new Container(x => {
                x.For<IRepository>().Use<Repository>();                
            });
        }
    }
}
