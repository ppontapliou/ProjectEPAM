using Backend.Interfaces;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class ImageController : ApiController
    {
        readonly IRepository _repository;
        public ImageController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Image/Test")]
        public IHttpActionResult Test()
        {
            return Ok(DBHelper.ImageCount(5));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Image/GetImages/{id:int}")]
        public IHttpActionResult GetImages(int id)
        {
            return Ok(_repository.GetImages(id));
        }

        [HttpPost]
        [Authorize]
        [Route("api/Image/AddImage")]
        public async Task<IHttpActionResult> AddImage()
        {
            try
            {
                List<string> files = new List<string>();
                HttpContext ctx = HttpContext.Current;
                Parameter parameter = new Parameter()
                {
                    Id = Convert.ToInt32(ctx.Request["Id"])
                };
                string root = ctx.Server.MapPath("~/App_Data");
                MultipartFormDataStreamProvider provider =
                    new MultipartFormDataStreamProvider(root);

                await Request.Content.ReadAsMultipartAsync(provider);
                parameter.Name = ImageSaver.SaveImage(provider, root);

                _repository.AddImage(parameter, User.Identity.Name, User.IsInRole("Admin") || User.IsInRole("Editor"));

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

        [HttpDelete]
        [Authorize]
        [Route("api/Image/DeleteImage/{id:int}/{idAd:int}")]
        public IHttpActionResult DeleteImage(int id, int idAd)
        {
            try
            {
                _repository.DeleteImages(idAd, id, User.Identity.Name, User.IsInRole("Admin") || User.IsInRole("Editor"));
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Image/GetImage")]
        public HttpResponseMessage GetImage(string name)
        {
            byte[] imgData;
            imgData = File.ReadAllBytes("G:/Мои документы/ProjectPivtures/" + name);
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
    }
}
