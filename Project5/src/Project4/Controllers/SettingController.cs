using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Project5.Models;
using Project5.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project5.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SettingController : Controller
    {
        private ISettingRepository _repository;
        private string userName;

        public SettingController(ISettingRepository repository)
        {
            _repository = repository;
        }

        //GET /api/setting
        [HttpGet]
        public IEnumerable<Setting> GetWindow()
        {
            userName = User.Identity.Name;
            return _repository.GetWindow(userName);
        }

        //POST: /api/setting
        [HttpPost]
        public void Create()
        {
            Setting s = new Setting();
            s.UserName = User.Identity.Name;
            s.warningWindow = 48;
            _repository.Create(s);
        }

        //PUT /api/setting
        [HttpPut]
        public void Update([FromBody]Setting setting)
        {
            userName = User.Identity.Name;
            _repository.Update(setting, userName);
        }
    }
}
