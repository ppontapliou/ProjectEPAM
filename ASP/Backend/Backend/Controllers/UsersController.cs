using Backend.Interfaces;
using Backend.Models;
using System.Collections.Generic;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Backend.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        IRepository _repository;
        public UsersController(IRepository repository)
        {
            _repository = repository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetUsers/{id:int}/name/{name}")]
        public IHttpActionResult GetUsers(int id, string name)
        {
            return Ok(_repository.GetUsers(id, name == "\"\"\"" ? "" : name));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(Contact contact)
        {
            try
            {
                _repository.CreateUser(contact);
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
        [AllowAnonymous]
        [HttpPost]
        [Route("Registration")]
        public IHttpActionResult RegistrateUser([FromBody]Contact contact)
        {
            try
            {
                contact.Role = "User";
                _repository.CreateUser(contact);
                return Ok();
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                return BadRequest("Такой логин существует");
            }
            catch (FormatException)
            {
                return BadRequest("Проверьте ваши данные");
            }
            catch
            {
                return InternalServerError();
            }
            
        }
        [Authorize]
        [HttpPut]
        [Route("ChangeUser")]
        public IHttpActionResult ChangeUser(Contact contact)
        {
            try
            {
                if (!User.IsInRole("Admin") && contact != null)
                {
                    contact.Login = User.Identity.Name;
                }
                _repository.ChangeUser(contact, User.IsInRole("Admin"));
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
        [HttpGet]
        [Route("GetPhones")]
        public IHttpActionResult GetPhones()
        {
            try
            {
                return Ok(_repository.GetPhones(User.Identity.Name));
            }
            catch
            {
                return InternalServerError();
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetMails")]
        public IHttpActionResult GetMails()
        {
            try
            {
                return Ok(_repository.GetMails(User.Identity.Name));
            }
            catch
            {
                return InternalServerError();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteUser/{id:int}")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                _repository.DeleteUser(id);
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
        [HttpPost]
        [Route("AddUserPhones")]
        public IHttpActionResult AddUserPhones([FromBody] Contact contact)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    contact.Login = User.Identity.Name;
                }
                _repository.AddPhone(contact, User.IsInRole("Admin"));
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
        [HttpPost]
        [Route("AddUserMails")]
        public IHttpActionResult AddUserMails([FromBody] Contact contact)
        {
            try
            {

                contact.Login = User.Identity.Name;
                _repository.AddMail(contact, User.IsInRole("Admin"));
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
        [Route("ChangeUserPhones")]
        public IHttpActionResult ChangeUserPhones([FromBody]Contact contact)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    contact.Login = User.Identity.Name;
                }
                _repository.ChangePhone(contact, User.IsInRole("Admin"));
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
        [Route("ChangeUserMails")]
        public IHttpActionResult ChangeUserMails([FromBody] Contact contact)
        {
            try
            {
                contact.Login = User.Identity.Name;
                _repository.ChangeMail(contact, User.IsInRole("Admin"));
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
        [Route("DeleteUserPhones/{id:int}")]
        public IHttpActionResult DeleteUserPhones(int id)
        {
            try
            {
                Contact contact = new Contact
                {
                    Login = User.Identity.Name,
                    Phones = new List<Parameter>()
                };
                contact.Phones.Add(new Parameter()
                {
                    Id = id
                });
                _repository.DeletePhone(contact, User.IsInRole("Admin"));
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
        [Route("DeleteUserMails/{id:int}")]
        public IHttpActionResult DeleteUserMails(int id)
        {
            try
            {
                Contact contact = new Contact
                {
                    Login = User.Identity.Name,
                    Mails = new List<Parameter>()
                };
                contact.Mails.Add(new Parameter()
                {
                    Id = id
                });
                _repository.DeleteMail(contact, User.IsInRole("Admin"));
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
