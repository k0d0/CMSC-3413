using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project5.Models;

namespace Project5.Repositories
{
    public interface ITodoRepository
    {
        void Create(Todo todo);

        void Delete(int id);

        void Update(Todo todo);

        IEnumerable<Todo> List(string status, string userName);

        Todo FindById(int id);

        IEnumerable<Todo> FindByTag(string queryString, string userName);

        IEnumerable<Todo> SortDesc(string order, string userName);

        IEnumerable<Todo> SortDue(string order, string userName);
    }
}
