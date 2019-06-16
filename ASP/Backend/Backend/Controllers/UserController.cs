using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        [HttpGet]
        [ResponseType(typeof(Contact))]
        public string Get(Contact contact)
        {
            return DBHelper.GetContactsInfo("exec Authentication " + contact.LoginAndPassword);
        }

        // GET: api/User/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/User
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public void Post(Contact contact)
        {
            DBHelper.CerateContact( contact);
        }

        // PUT: api/User/5
        [HttpPut]
        [ResponseType(typeof(Contact))]
        public void Put(Contact contact)
        {
            DBHelper.ChangeUserName(contact);
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
