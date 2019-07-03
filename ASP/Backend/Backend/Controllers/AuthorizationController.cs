using Backend.Interfaces;
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
    public class AuthorizationController : ApiController
    {
        IRepository _repository;

        public AuthorizationController(IRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Authorization
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authorization/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authorization
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(Contact contact)
        {
            return Ok(_repository.GetUser(contact));
        }

        // PUT: api/Authorization/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authorization/5
        public void Delete(int id)
        {
        }
    }
}