using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.dbl.repo;
using api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContentController : ControllerBase
    {
        private ContentRepo ContentRepo;
        private ILogger<ContentController> _log;

        public ContentController(IConfiguration config, ILogger<ContentController> log)
        {
            ContentRepo = new ContentRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/content/")]
        public IActionResult GetContent()
        {
            _log.LogInformation(ContentRepo.getConnectionString());
            return Ok(ContentRepo.FindAll());
        }
        [HttpGet("~/api/content/{id}")]
        public IActionResult GetContent(int id)
        {
            return Ok(ContentRepo.FindByID(id));
        }

        [HttpPost("~/api/content/")]
        public void AddContent([FromBody]Content data)
        {
            ContentRepo.Add(data);
            _log.LogInformation($"The Content that is added {JsonConvert.SerializeObject(data)}");

        }

        [HttpPut("~/api/content/{id}")]
        public void Put(int id, [FromBody]Content data)
        {

            ContentRepo.Update(data);
            _log.LogInformation($"The Content that was updated {JsonConvert.SerializeObject(data)}");

        }
        [HttpDelete("~/api/content/{id}")]
        public void Delete(int id)
        {
            ContentRepo.Remove(id);
            _log.LogInformation($"The Content that was deleted {id.ToString()}");

        }
    }
}