using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project4.Models;

namespace Project4.Repository
{
    public interface IProjectRepository
    {
        Project Add(Project project);
        IEnumerable<Project> GetAll();
        void Remove(int projectId);
        void Update(Project project);
    }
}
