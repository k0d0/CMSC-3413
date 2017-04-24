using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Project5.Models;

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private SignInManager<TodoUser> _signInManager;

        public LoginController(SignInManager<TodoUser> signinManager)
        {
            _signInManager = signinManager;
        }
        // POST: api/login
        [HttpPost]
        public async Task<HttpStatusCodeResult> Login([FromBody] LoginModel loginModel)
        {
            var signinResult = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true,
                false);
            if (!signinResult.Succeeded)
            {
                return new HttpUnauthorizedResult();
            }
            return new HttpOkResult();
        }
    }
}