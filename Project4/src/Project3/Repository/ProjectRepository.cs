using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project4.Models;

namespace Project4.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private MyDbContext context;
        public ProjectRepository(MyDbContext context)
        {
            this.context = context;
        }

        public Project Add(Project project)
        {
            this.context.Projects.Add(project);
            this.context.SaveChanges();
            return project;
        }

        public IEnumerable<Project> GetAll()
        {
            return this.context.Projects.ToList();
            //return this.context.Projects.Include(p=>p.Tags);
        }

        public void Remove(int projectId)
        {
            this.context.Projects.Remove(this.context.Projects.FirstOrDefault(p => p.ProjectId == projectId));
            this.context.SaveChanges();
        }

        public void Update(Project project)
        {
            var existingProject = this.context.Projects.First(p => p.ProjectId == project.ProjectId);
            existingProject.Description = project.Description;
            existingProject.Name = project.Name;
            existingProject.DueDate = project.DueDate;
            this.context.SaveChanges();
        }
    }
}
