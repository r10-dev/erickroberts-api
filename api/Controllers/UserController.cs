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
    public class UserController : ControllerBase
    {
        private UserRepo UserRepo;
        private ILogger<UserController> _log;

        public UserController(IConfiguration config, ILogger<UserController> log)
        {
            UserRepo = new UserRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/User/")]
        public IActionResult GetUser()
        {
            _log.LogInformation(UserRepo.getConnectionString());
            return Ok(UserRepo.FindAll());
        }
        [HttpGet("~/api/User/{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(UserRepo.FindByID(id));
        }

        [HttpPost("~/api/User/")]
        public void AddUser([FromBody]User data)
        {
            UserRepo.Add(data);
            _log.LogInformation($"The User that is added {JsonConvert.SerializeObject(data)}");

        }

        [HttpPut("~/api/User/{id}")]
        public void Put(int id, [FromBody]User data)
        {

            UserRepo.Update(data);
            _log.LogInformation($"The User that was updated {JsonConvert.SerializeObject(data)}");

        }
        [HttpDelete("~/api/User/{id}")]
        public void Delete(int id)
        {
            UserRepo.Remove(id);
            _log.LogInformation($"The User that was deleted {id.ToString()}");

        }
    }
}