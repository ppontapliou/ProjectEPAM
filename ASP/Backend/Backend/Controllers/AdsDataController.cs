using Backend.Interfaces;
using Backend.Models;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class AdsDataController : ApiController
    {
        IRepository _repository;
        public AdsDataController(IRepository repository)
        {
            _repository = repository;            
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        [Route("api/AdsData/AddType")]
        public IHttpActionResult AddType([FromBody]Parameter parameter)
        {
            try
            {
                _repository.AddType(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        [Route("api/AdsData/AddCategory")]
        public IHttpActionResult AddCategory([FromBody]Parameter parameter)
        {
            try
            {
                _repository.AddCategory(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        [Route("api/AdsData/AddState")]
        public IHttpActionResult AddState([FromBody]Parameter parameter)
        {
            try
            {
                _repository.AddState(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        // [Authorize(Roles = "Admin, Editor")]
        [AllowAnonymous]
        [HttpPut]
        [Route("api/AdsData/ChangeType")]
        [ResponseType(typeof(Parameter))]
        public IHttpActionResult ChangeType([FromBody]Parameter parameter)
        {
            try
            {
                _repository.ChangeType(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPut]
        [Route("api/AdsData/ChangeCategory")]
        public IHttpActionResult ChangeCategory([FromBody]Parameter parameter)
        {
            try
            {
                _repository.ChangeCategory(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPut]
        [Route("api/AdsData/ChangeState")]
        public IHttpActionResult ChangeState([FromBody]Parameter parameter)
        {
            try
            {
                _repository.ChangeState(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        [Route("api/AdsData/DeleteType")]
        public IHttpActionResult DeleteType(Parameter parameter)
        {
            try
            {
                _repository.DeleteType(parameter);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        [Route("api/AdsData/DeleteCategory/{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            try
            {
                _repository.DeleteCategory(id);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpDelete]
        [Route("api/AdsData/DeleteState/{id:int}")]
        public IHttpActionResult DeleteState(int id)
        {
            try
            {
                _repository.DeleteState(id);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/AdsData/GetTypes")]
        public IHttpActionResult GetTypes()
        {
            try
            {
                return Ok(_repository.GetTypes());
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

        //[Authorize(Roles = "Admin, Editor")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/AdsData/GetCategories")]
        public IHttpActionResult GetCategories()
        {
            try
            {
                return Ok(_repository.GetCategories());
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
        [Route("api/AdsData/GetStates")]
        public IHttpActionResult GetStates()
        {
            try
            {
                return Ok(_repository.GetStates());
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
