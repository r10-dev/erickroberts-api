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
    public class CommentController : ControllerBase
    {
        private CommentRepo CommentRepo;
        private ILogger<CommentController> _log;

        public CommentController(IConfiguration config, ILogger<CommentController> log)
        {
            CommentRepo = new CommentRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/comment/")]
        public IActionResult GetComments()
        {
            _log.LogInformation(CommentRepo.getConnectionString());
            return Ok(CommentRepo.FindAll());
        }
        [HttpGet("~/api/comment/{id}")]
        public IActionResult GetComments(int id)
        {
            return Ok(CommentRepo.FindByID(id));
        }

        [HttpPost("~/api/Comment/")]
        public void AddComment([FromBody]Comment data)
        {
            CommentRepo.Add(data);
            _log.LogInformation($"The Comment that is added {JsonConvert.SerializeObject(data)}");

        }

        [HttpPut("~/api/comment/{id}")]
        public void Put(int id, [FromBody]Comment data)
        {

            CommentRepo.Update(data);
            _log.LogInformation($"The Comment that was updated {JsonConvert.SerializeObject(data)}");

        }
        [HttpDelete("~/api/comment/{id}")]
        public void Delete(int id)
        {
            CommentRepo.Remove(id);
            _log.LogInformation($"The Comment that was deleted {id.ToString()}");

        }
    }
}
