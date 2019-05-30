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
    public class RoleController : ControllerBase
    {
        private RoleRepo RoleRepo;
        private ILogger<RoleController> _log;

        public RoleController(IConfiguration config, ILogger<RoleController> log)
        {
            RoleRepo = new RoleRepo(config);
            _log = log;
        }
        [HttpGet("~/api/getall/Role/")]
        public IActionResult GetRole()
        {
            _log.LogInformation(RoleRepo.getConnectionString());
            return Ok(RoleRepo.FindAll());
        }
        [HttpGet("~/api/Role/{id}")]
        public IActionResult GetRole(int id)
        {
            return Ok(RoleRepo.FindByID(id));
        }

        [HttpPost("~/api/Role/")]
        public void AddRole([FromBody]Role data)
        {
            RoleRepo.Add(data);
            _log.LogInformation($"The Role that is added {JsonConvert.SerializeObject(data)}");

        }

        [HttpPut("~/api/Role/{id}")]
        public void Put(int id, [FromBody]Role data)
        {

            RoleRepo.Update(data);
            _log.LogInformation($"The Role that was updated {JsonConvert.SerializeObject(data)}");

        }
        [HttpDelete("~/api/Role/{id}")]
        public void Delete(int id)
        {
            RoleRepo.Remove(id);
            _log.LogInformation($"The Role that was deleted {id.ToString()}");

        }
    }
}