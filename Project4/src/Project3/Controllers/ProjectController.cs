using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project4.Repository;
using Project4.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Project4.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(projectRepository.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Project project)
        {
            var newProject = projectRepository.Add(project);
            return Created("api/project", project);
        }


        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Project value)
        {
            projectRepository.Update(value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            projectRepository.Remove(id);
            return Ok();
        }
    }
}
