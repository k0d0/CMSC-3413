using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Project5.Models;

namespace Project5.Models
{
    public class TodoAppSeedData
    {
        private TodoContext _context;
        //private SettingContext _settingContext;
        private UserManager<TodoUser> _userManager;

        public TodoAppSeedData(TodoContext todoContext,  UserManager<TodoUser> userManager)
        {
            _context = todoContext;
            //_settingContext = settingContext;
            _userManager = userManager;
        }

        public async Task SeedData()
        {
            if(await _userManager.FindByNameAsync("dcordova") == null)
            {
                var dallenUser = new TodoUser()
                {
                    UserName = "dcordova",
                    Email = "dcordova@uco.edu"
                };
                IdentityResult x = await _userManager.CreateAsync(dallenUser, "Banner11");
            }

            if(await _userManager.FindByNameAsync("NewGuy") == null)
            {
                var newUser = new TodoUser()
                {
                    UserName = "NewGuy",
                };
                IdentityResult x = await _userManager.CreateAsync(newUser, "Banner00");
            }

            if(!_context.Todos.Any())
            {
                _context.Add(new Todo()
                {
                    Description = "Only I can see this",
                    DueDate = DateTime.Now,
                    Tags = "Tag1 Tag2",
                    Status = "INCOMPLETE",
                    UserName = "dcordova"
                });
                _context.Add(new Todo()
                {
                    Description = "Become OldGuy.",
                    DueDate = new DateTime(2016, 4, 30, 10, 0, 0),
                    Tags = "Tag1 Tag3",
                    Status = "INCOMPLETE",
                    UserName = "NewGuy"
                });

                _context.SaveChanges();
            }

            if(!_context.Settings.Any())
            {
                _context.Add(new Setting
                {
                    warningWindow = 48,
                    UserName = "dcordova"
                });

                _context.Add(new Setting
                {
                    warningWindow = 24,
                    UserName = "NewGuy"
                });

                _context.SaveChanges();
            }
            
        }
    }
}
