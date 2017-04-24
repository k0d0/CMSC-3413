using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Project5.Models;
using Project5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : Controller
    {
        private ITodoRepository _repository;
        private string UserName;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        //GET api/todo/complete
        [HttpGet("complete")]
        public IEnumerable<Todo> GetCompleteTodos()
        {
            UserName = User.Identity.Name;
            return _repository.List("COMPLETE", UserName);
        }

        //GET api/todo/
        [HttpGet]
        public IEnumerable<Todo> GetTodos()
        {
            UserName = User.Identity.Name;
            var todos = _repository.List("INCOMPLETE", UserName);
            return todos;
        }

        //GET api/todo/search/{tags}
        [HttpGet("search/{tags}")]
        public IEnumerable<Todo> GetSearchResults(string tags)
        {
            UserName = User.Identity.Name;
            var todos = _repository.FindByTag(tags, UserName);
            return todos;
        }

        //GET api/todo/desc/asc
        [HttpGet("desc/{order}")]
        public IEnumerable<Todo> GetDescSorted(string order)
        {
            UserName = User.Identity.Name;
            return _repository.SortDesc(order,UserName);
        }

        //GET api/todo/date/asc
        [HttpGet("date/{order}")]
        public IEnumerable<Todo> GetDateSorted(string order)
        {
            UserName = User.Identity.Name;
            return _repository.SortDue(order, UserName);
        }

        //POST api/todo
        [HttpPost]
        public void Post([FromBody]Todo todo)
        {
            todo.UserName = User.Identity.Name;
            Response.StatusCode = (int)HttpStatusCode.Created;
            _repository.Create(todo);
        }

        //PUT api/todo
        [HttpPut()]
        public void Put([FromBody]Todo todo)
        {
            _repository.Update(todo);
        }

        //DELETE api/todo/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
