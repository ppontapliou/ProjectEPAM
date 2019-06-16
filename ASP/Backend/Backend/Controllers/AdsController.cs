using Backend.Interfaces;
using Backend.Models;
using Newtonsoft.Json;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Script.Serialization;


namespace Backend.Controllers
{
    public class RegisterByContainer
    {
        public IContainer Container;
        //unity container
        //iactionresult
        public RegisterByContainer( )
        {
            Container = new Container(x => {
                x.For<IRepository>().Use<Repository>();
                
            });
        }
    }
    public class AdsController : ApiController
    {
        // GET: api/Ads
        IRepository _repository;

        public AdsController()
        {
            var container = new RegisterByContainer().Container;
            var class1Inst = container.GetInstance<IRepository>();
        }
        public IHttpActionResult Get()
        {           
            JavaScriptSerializer serializer = new JavaScriptSerializer();            
            var result = new Ads(DBHelper.GetAds("exec GetAds")).Ad;
            return Ok(result);
        }

        // GET: api/Ads/5
        public string Get(int id)
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(new Ads(DBHelper.GetAds("exec GetAd "+id)).Ad[0]);
        }

        // POST: api/Ads
        [HttpPost]
        [ResponseType(typeof(Ad))]
        public void Post(Ad value)
        {
            DBHelper.AddAd(value);
        }

        // PUT: api/Ads/5
        [HttpPut]
        [ResponseType(typeof(Ad))]
        public void Put(Ad value)
        {
            DBHelper.UpdateAd(value);
        }

        // DELETE: api/Ads/5
        public void Delete(int id)
        {
            DBHelper.DeleteAd(id);
        }
    }
}
