using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project4.Models;
using Project3.Models;

namespace Project4.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(MyDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Projects.Any())
            {
                return;
            }

            Project project1 = new Project()
            {
                Description = "This is project 1",
                Name = "Project 1",
                DueDate = DateTime.UtcNow
            };
            context.Projects.Add(project1);

            Tag tag1 = new Tag()
            {
                Name = "My Project Tag",
                Project = project1
            };

            context.Tags.Add(tag1);
            context.SaveChanges();
        }
    }
}
