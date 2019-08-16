using Backend.Interfaces;
using Backend.Models;
using System;
using System.Web.Http;
using System.Web.Http.Description;
using Unity;

namespace Backend.Controllers
{
    public class AdsController : ApiController
    {
        IRepository _repository;
        public AdsController(IRepository repository)
        {
            _repository = repository;

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ads")]
        public IHttpActionResult GetAds(string type = null, string category = null)
        {
            try
            {
                return Ok(_repository.GetAds(type, category).Ad);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Ads/{id:int}")]
        public IHttpActionResult GetAd(int id)
        {
            try
            {
                return Ok(_repository.GetAd(id));
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Ads/UserAds")]
        public IHttpActionResult GetUserAds()
        {
            try
            {
                return Ok(_repository.GetUserAds(User.Identity.Name));
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Ad))]
        public IHttpActionResult AddAd([FromBody]Ad ad)
        {
            try
            {
                ad.Contact = new Contact()
                {
                    Login = User.Identity.Name
                };
                _repository.AddAd(ad);
                return Ok();
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPut]
        [ResponseType(typeof(Ad))]
        public IHttpActionResult ChangeAd([FromBody]Ad ad)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    ad.Contact = new Contact()
                    {
                        Login = User.Identity.Name
                    };
                }
                _repository.ChangeAd(ad, User.IsInRole("Admin"));
                return Ok();
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteAd(int id)
        {
            try
            {
                _repository.DeleteAd(id,User.Identity.Name, User.IsInRole("Admin"));
                return Ok();
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
