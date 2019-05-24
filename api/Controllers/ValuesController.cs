using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.dbl.repo;
using Microsoft.Extensions.Configuration;
using api.models;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ControllerBase
    {
        private AuthorRepo authorRepo;
        ILogger _log;
        public ValuesController(IConfiguration config, ILogger<ValuesController> log){
            authorRepo = new AuthorRepo(config);
            _log = log;
        }

        [HttpGet("~/api/getall/authors/")]
        public IActionResult GetAuthors(){
            _log.LogInformation(authorRepo.getConnectionString());
            return Ok(authorRepo.FindAll());
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
