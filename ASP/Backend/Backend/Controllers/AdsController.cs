using Backend.Interfaces;
using Backend.Models;
using System.Web.Http;
using System.Web.Http.Description;


namespace Backend.Controllers
{

    public class AdsController : ApiController
    {
        // GET: api/Ads
        IRepository _repository;

        public AdsController(IRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {           
            return Ok(_repository.GetAds());
        }

        // GET: api/Ads/5
        public IHttpActionResult Get(int id)
        {           
            var result = new Ads(DBHelper.GetAds("exec GetAd " + id)).Ad[0];
            return Ok(_repository.GetAd(id));
        }

        // POST: api/Ads
        [HttpPost]
        [ResponseType(typeof(Ad))]
        public IHttpActionResult Post([FromBody]Ad value)
        {
            _repository.PostAd(value);           
            return Ok();
        }

        // PUT: api/Ads/5
        [HttpPut]
        [ResponseType(typeof(Ad))]
        public IHttpActionResult Put(Ad value)
        {
            _repository.PutAd(value);            
            return Ok();
        }

        // DELETE: api/Ads/5
        [HttpDelete]
        [ResponseType(typeof(Ad))]
        public IHttpActionResult Delete(Ad value)
        {
            _repository.DeleteAd(value);
          
            return Ok();
        }
    }
}
