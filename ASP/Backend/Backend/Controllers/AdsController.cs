using Backend.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class AdsController : ApiController
    {
        readonly IRepository _repository;
        public AdsController(IRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpPost]
        [Route("api/Ads/Image")]
        public async Task<IHttpActionResult> Image()
        {
            List<string> files = new List<string>();
            HttpContext ctx = HttpContext.Current;
            string line = ctx.Request["Adress"];
            string root = ctx.Server.MapPath("~/App_Data");
            MultipartFormDataStreamProvider provider =
                new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                files = ImageSaver.SaveImages(provider, root);
                Ad ad = new Ad()
                {
                    Contact = new Contact()
                    {
                        Login = User.Identity.Name
                    },
                    Adress = ctx.Request["Adress"],
                    Category = ctx.Request["Category"],
                    Name = ctx.Request["Name"],
                    Picture = files[0],
                    State = ctx.Request["State"],
                    Title = ctx.Request["Title"],
                    Type = ctx.Request["Type"],
                };
                int id = _repository.AddAd(ad);
                for (int i = 1; i < files.Count; i++)
                {
                    _repository.AddImage(new Parameter() { Id = id, Name = files[i] }, User.Identity.Name, false);
                }

            }
            catch (FileLoadException)
            {
                return BadRequest("Consists not image files");
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
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
        [Route("api/Ads/PartAds/{id:int}/name/{name}/category/{category:int}/state/{state:int}/type/{type:int}")]
        public IHttpActionResult PartAds(int id, int category, int state, int type, string name="")
        {
            try
            {
                return Ok(_repository.GetPartAds(id, category, state, name=="\"\"\""?"":name, type).Ad);
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
                return Ok(_repository.GetUserAds(User.Identity.Name).Ad);
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
        [Route("api/Ads")]
        public async Task<IHttpActionResult> ChangeAd()
        {
            HttpContext ctx = HttpContext.Current;
            string line = ctx.Request["Adress"];
            string root = ctx.Server.MapPath("~/App_Data");
            MultipartFormDataStreamProvider provider =
                new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                Ad ad = new Ad()
                {
                    Contact = new Contact()
                    {
                        Login = User.Identity.Name
                    },
                    Id = Convert.ToInt32(ctx.Request["Id"]),
                    Adress = ctx.Request["Adress"],
                    Category = ctx.Request["Category"],
                    Name = ctx.Request["Name"],
                    Picture = ctx.Request["Picture"],
                    State = ctx.Request["State"],
                    Title = ctx.Request["Title"],
                    Type = ctx.Request["Type"],
                };
                if (ad.Picture == "")
                {
                    ad.Picture = ImageSaver.SaveImage(provider, root);
                }

                _repository.ChangeAd(ad, User.IsInRole("Admin")|| User.IsInRole("Editor"));
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
        [Route("api/Ads/{id:int}")]
        public IHttpActionResult DeleteAd(int id)
        {
            try
            {
                _repository.DeleteAd(id, User.Identity.Name, User.IsInRole("Admin")|| User.IsInRole("Editor"));
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
