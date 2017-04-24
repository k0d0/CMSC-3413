using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using Project5.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project5.Models
{
    public class TodoContext : IdentityDbContext<TodoUser>
    {
        public TodoContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Startup.Configuration["Data:TodoAppConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
