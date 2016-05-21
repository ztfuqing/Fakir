using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Abp.WebApi.Controllers;
using Fakir.Metting.Services;
using Newtonsoft.Json;

namespace Fakir.Metting.Controller
{
    public class FileUploadController : AbpApiController
    {
        private readonly IAgendaService _agendaService;

        public FileUploadController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [Route("Image")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFileUpload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/files");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["agendaId"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var agendaId = result.FormData["agendaId"];


            var fileName = result.FileData.First().Headers.ContentDisposition.FileName;
            var originalFileName = JsonConvert.DeserializeObject(fileName).ToString();
            var localFileName = result.FileData.First().LocalFileName;
            var newName = originalFileName;
            if (File.Exists(Path.Combine(root, newName)))
            {
                newName = Guid.NewGuid().ToString("N") + originalFileName;
            }
            
            FileInfo fi = new FileInfo(Path.Combine(root, localFileName));
            fi.MoveTo(Path.Combine(root, newName));

            await _agendaService.SaveFile(new Dtos.Agendas.AgendaFileDto
            {
                AgendaId = int.Parse(agendaId),
                FileName = originalFileName,
                FileUrl = "/files/" + newName
            });

            return Request.CreateResponse(HttpStatusCode.OK, "success!");

        }
    }
}
