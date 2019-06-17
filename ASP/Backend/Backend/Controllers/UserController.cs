using Backend.Interfaces;
using Backend.Models;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class UserController : ApiController
    {
        IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/User
        [HttpGet]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Get(Contact contact)
        {
            return Ok(_repository.GetUser(contact));
        }                

        // POST: api/User
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(Contact contact)
        {
            _repository.PostUser(contact);
            return Ok();
        }

        // PUT: api/User/5
        [HttpPut]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Put(Contact contact)
        {
            _repository.PutUser(contact);
            return Ok();
        }

        // DELETE: api/User/5
        [HttpDelete]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Delete(int id,[FromBody]Contact contact)
        {
            _repository.DeleteUser(id,contact);
            return Ok();
        }
    }
}
