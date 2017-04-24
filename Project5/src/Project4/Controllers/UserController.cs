using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Project5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserManager<TodoUser> _userManager;

        public UserController(UserManager<TodoUser> userManager)
        {
            _userManager = userManager;
        }
        // POST: api/user/create
        [HttpPost("create")]
        public async Task<HttpStatusCodeResult> Create([FromBody] TodoUser newUser)
        {
            if (await _userManager.FindByNameAsync(newUser.UserName) == null)
            {
                var createResult = await _userManager.CreateAsync(newUser, newUser.Password);
                if (!createResult.Succeeded)
                {
                    return new HttpStatusCodeResult(400);
                }
                return new HttpStatusCodeResult(201);
            }
            else
                return new HttpStatusCodeResult(409);
        }
    }
}
