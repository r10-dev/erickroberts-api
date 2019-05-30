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
    public class AuthorsController : ControllerBase
    {
        private AuthorRepo authorRepo;
        private ILogger<AuthorsController> _log;

        public AuthorsController(IConfiguration config, ILogger<AuthorsController> log)
        {
            authorRepo = new AuthorRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/author/")]
        public IActionResult GetAuthors()
        {
            
            return Ok(authorRepo.FindAll());
        }
        [HttpGet("~/api/author/{id}")]
        public IActionResult GetAuthors(int id)
        {
            return Ok(authorRepo.FindByID(id));
        }

        [HttpPost("~/api/author/")]
        public bool AddAuthor ([FromBody]Author data)
        {
            authorRepo.Add(data);
            _log.LogInformation($"The author that is added {JsonConvert.SerializeObject(data)}");
            
            return true;
        }

        [HttpPut("~/api/author/{id}")]
        public void Put(int id, [FromBody]Author data)
        {

            authorRepo.Update(data);
            _log.LogInformation($"The author that was updated {JsonConvert.SerializeObject(data)}");
            
        }
        [HttpDelete("~/api/author/{id}")]
        public void Delete(int id)
        {
            authorRepo.Remove(id);
            _log.LogInformation($"The author that was deleted {id.ToString()}");
            
        }
    }
}