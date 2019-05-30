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
    public class ContentTagsController : ControllerBase
    {

        private ContentTagsRepo ContentTagsRepo;
        private ILogger<ContentTagsController> _log;

        public ContentTagsController(IConfiguration config, ILogger<ContentTagsController> log)
        {
            ContentTagsRepo = new ContentTagsRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/content_tags/")]
        public IActionResult GetContentTags()
        {
            _log.LogInformation(ContentTagsRepo.getConnectionString());
            return Ok(ContentTagsRepo.FindAll());
        }
        [HttpGet("~/api/content_tags/{id}")]
        public IActionResult GetContentTags(int id)
        {
            return Ok(ContentTagsRepo.FindByID(id));
        }

        [HttpPost("~/api/content_tags/")]
        public void AddContentTags([FromBody]ContentTags data)
        {
            ContentTagsRepo.Add(data);
            _log.LogInformation($"The ContentTags that is added {JsonConvert.SerializeObject(data)}");

        }

        [HttpPut("~/api/content_tags/{id}")]
        public void Put(int id, [FromBody]ContentTags data)
        {

            ContentTagsRepo.Update(data);
            _log.LogInformation($"The ContentTags that was updated {JsonConvert.SerializeObject(data)}");

        }
        [HttpDelete("~/api/content_tags/{id}")]
        public void Delete(int id)
        {
            ContentTagsRepo.Remove(id);
            _log.LogInformation($"The ContentTags that was deleted {id.ToString()}");

        }
    }
}
