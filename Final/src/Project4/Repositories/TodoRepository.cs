using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;


namespace Project5.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public void Create(Todo todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var todoToDelete = FindById(id);
            if(todoToDelete != null)
            {
                _context.Todos.Remove(todoToDelete);
                _context.SaveChanges();
            }
        }

        public Todo FindById(int id)
        {
            var todo = _context.Todos.First(t => t.Id == id);
            return todo;
        }

        public IEnumerable<Todo> FindByTag( string queryString, string userName)
        {
            string[] tags = queryString.Split(' ');
            var result = _context.Todos.Where(d=> tags.Any(t => d.Tags.Contains(t)) && d.UserName == userName);
            return result;
        }

        public IEnumerable<Todo> List(string status, string userName)
        {
            return _context.Todos.Where(t => t.Status == status && t.UserName == userName);
        }

        public void Update(Todo todo)
        {
            var todoToUpdate = FindById(todo.Id);
            todoToUpdate.Description = todo.Description;
            todoToUpdate.DueDate = todo.DueDate;
            todoToUpdate.Tags = todo.Tags;
            todoToUpdate.Status = todo.Status;
            _context.SaveChanges();
        }

        public IEnumerable<Todo> SortDesc(string order, string userName)
        {
            if (order == "asc") { 
                var temp =_context.Todos.OrderBy(t => t.Description);
                return temp.Where(t => t.UserName == userName);
            }
            else
            { 
                var temp = _context.Todos.OrderByDescending(t => t.Description);
                return temp.Where(t => t.UserName == userName);
            }
        }

        public IEnumerable<Todo> SortDue(string order, string userName)
        {
            if (order == "asc")
            { 
                var temp = _context.Todos.OrderBy(t => t.DueDate);
                return temp.Where(t => t.UserName == userName);
            }
            else
            { 
                var temp = _context.Todos.OrderByDescending(t => t.DueDate);
                return temp.Where(t => t.UserName == userName);
            }
        }
    }
}
